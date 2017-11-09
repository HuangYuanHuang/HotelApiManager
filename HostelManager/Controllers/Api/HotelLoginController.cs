using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelManager.Models;
using Microsoft.EntityFrameworkCore;
using HostelManager.Common;

namespace HostelManager.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/HotelLogin")]
    public class HotelLoginController : BaseApiController
    {


        /// <summary>
        /// 酒店账号登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public object Post([FromBody]LoginHotelModel login)
        {
            var pwd = DESHelper.MD5Encrypt(login.password, DESHelper.GetKey());
            var model = hostelContext.Accouts.FirstOrDefault(d => d.LoginName == login.username && d.Pwd == pwd);
            if (model == null)
            {
                return new { state = false, message = "账号密码错误!" };
            }
            try
            {
                var hotelModel = hostelContext.Hotels.Include(d => d.Area).FirstOrDefault(d => d.Id == model.HotelId);
                return new { state = true, message = "登录成功", token = Guid.NewGuid().ToString("N"), data = hotelModel };
            }
            catch (Exception)
            {

                return new { state = false, message = "系统错误，未找到该账户管理的酒店信息，请联系管理员处理" };
            }
        }

        /// <summary>
        /// 酒店修改密码
        /// </summary>
        /// <param name="id">酒店id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public object Put(string id, [FromBody]UpdateHotelPwdModel model)
        {
            var res = hostelContext.Accouts.FirstOrDefault(d => d.GUID == id);
            if (res == null)
            {
                return new { state = false, message = "没有找到该账户信息" };
            }
            var oldPwd = DESHelper.MD5Decrypt(res.Pwd, DESHelper.GetKey());
            if (oldPwd != model.oldPassword)
            {
                return new { state = false, message = "原密码不正确" };
            }
            try
            {
                var newPassword = DESHelper.MD5Encrypt(model.newPassword, DESHelper.GetKey());
                res.Pwd = newPassword;
                hostelContext.Messages.Add(new HostelModel.MessageModel()
                {
                    Context="用户通过APP修改密码",
                    From="System",
                    To=id,
                    Type="系统消息"
                });
                hostelContext.SaveChanges();
                return new { state = true, message = "密码修改成功" };
            }
            catch (Exception)
            {

                return new { state = false, message = "系统错误,请稍后重试" };
            }

        }


    }
}
