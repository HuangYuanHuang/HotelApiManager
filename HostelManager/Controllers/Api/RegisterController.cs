using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using MessageWeb.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using HostelManager.Models;
using Newtonsoft.Json.Linq;

namespace HostelManager.Controllers.Api
{
    /// <summary>
    /// 用户注册接口
    /// </summary>
    
    [Produces("application/json")]
    [Route("api/Register")]
    
    public class RegisterController : BaseApiController
    {
        private readonly IOptions<JpushAppSettings> options;
        /// <summary>
        /// 构造函数 通过IOC获取配置节点
        /// </summary>
        /// <param name="_options"></param>
        public RegisterController(IOptions<JpushAppSettings> _options)
        {
            this.options = _options;
        }

        /// <summary>
        /// 用户注册发送手机验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        [HttpGet("{phone}")]
        public async Task<object> Get(string phone)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.Value.Base64);
                var httpContent = new StringContent($"{{\"mobile\":\"{phone}\",\"temp_id\":{options.Value.SmsCode.TemplateId}}}");
                var data = await httpClient.PostAsync(options.Value.SmsCode.ApiUrl, httpContent);
                var result = await data.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(result);
            }
        }

        /// <summary>
        /// 用户注册接口 
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public async Task<object> Post([FromBody]RegisterModel model)
        {
            using (HttpClient httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.Value.Base64);
                var httpContent = new StringContent($"{{\"code\":\"{model.Code}\"}}");
                var data = await httpClient.PostAsync(options.Value.SmsCode.VailUrl.Replace("{msg_id}", model.MsgId), httpContent);
                var result = await data.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(result);
                if (jo["is_valid"].ToString() == "True")
                {
                    var persons = hostelContext.ServicePersons.FirstOrDefault(d => d.Phone == model.Phone);
                    if (persons != null)
                    {
                        return new { state = false, message = "该手机号码已经注册,请直接登陆" };
                    }
                   
                    var registerResult = register(model);
                    if (registerResult)
                    {
                        return new { state = true, message = "注册成功" };
                    }

                    return new { state = false, message = "注册成功,服务器数据保存失败，请检查数据是否完整" };
                }
                else
                {
                    return new { state = false, message = "验证码错误" };
                }

            }
        }

        private bool register(RegisterModel model)
        {
            hostelContext.ServicePersons.Add(new HostelModel.ServicePersonModel()
            {
                RealName=model.RealName,
                IdentityCard=model.IdentityCard,
                Phone=model.Phone,
                Pwd=model.Pwd,
                Sex=model.Sex
            });

            try
            {
                hostelContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


    }
}
