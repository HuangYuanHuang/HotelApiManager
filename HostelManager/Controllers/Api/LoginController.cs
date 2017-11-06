using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MessageWeb.Config;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using HostelManager.Models;
using Newtonsoft.Json.Linq;

namespace HostelManager.Controllers.Api
{
    /// <summary>
    /// 用户登录API
    /// </summary>
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : BaseApiController
    {
        private readonly IOptions<JpushAppSettings> options;
        /// <summary>
        /// 构造函数 通过IOC获取配置节点
        /// </summary>
        /// <param name="_options"></param>
        public LoginController(IOptions<JpushAppSettings> _options)
        {
            this.options = _options;
        }


        /// <summary>
        /// 用户登陆发送手机验证码
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
        /// 用户登录
        /// </summary>
        /// <param name="model">登录模型</param>
        [HttpPost]
        public async Task<object> Post([FromBody]UserLoginModel model)
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
                    if (persons == null)
                    {
                        persons = new HostelModel.ServicePersonModel()
                        {
                            Phone = model.Phone,

                        };
                        hostelContext.ServicePersons.Add(persons);

                    }
                    string token = Guid.NewGuid().ToString("N");
                    var loginResult = login(model, token, persons.GUID);
                    if (loginResult)
                    {
                        return new { state = true, message = "登录成功", token = token, data = persons };
                    }

                    return new { state = false, message = "登录失败,服务器在线用户数据Error" };
                }
                else
                {
                    return new { state = false, message = "验证码错误" };
                }

            }
        }


        private bool login(UserLoginModel model, string token, string guid)
        {
            var obj = hostelContext.UserOnlines.FirstOrDefault(d => d.Phone == model.Phone);
            if (obj == null)
            {
                obj = new HostelModel.UserOnlineModel();
                hostelContext.UserOnlines.Add(obj);

            }
            obj.LastLogin = DateTime.Now;
            obj.Token = token;
            obj.IMEI = model.IMEI;
            obj.Phone = model.Phone;
            obj.SoftVersion = model.SoftVersion;
            obj.SystemType = model.SystemType;
            obj.Device = model.Device;
            obj.AccoutType = model.AccoutType;
            obj.UserGUID = guid;
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
