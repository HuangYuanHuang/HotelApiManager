using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelManager.Models;
using Microsoft.EntityFrameworkCore;

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
            var model = hostelContext.Accouts.FirstOrDefault(d => d.LoginName == login.username);
            if (model == null || login.password != "123")
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
        
      
    }
}
