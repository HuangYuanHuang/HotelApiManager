using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelModel;

namespace HostelManager.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/UserOnline")]
    public class UserOnlineController : BaseApiController
    {
        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UserOnlineModel> Get()
        {
            return hostelContext.UserOnlines.ToList();
        }


    }
}
