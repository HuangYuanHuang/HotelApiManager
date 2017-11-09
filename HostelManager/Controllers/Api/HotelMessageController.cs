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
    [Route("api/HotelMessage")]
    public class HotelMessageController : BaseApiController
    {
        /// <summary>
        /// 获取酒店的所有消息
        /// </summary>
        /// <param name="id">酒店GUID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IEnumerable<MessageModel> Get(string id)
        {
            return hostelContext.Messages.Where(d => d.To == id).OrderByDescending(d => d.CreateTime);
        }
    }
}
