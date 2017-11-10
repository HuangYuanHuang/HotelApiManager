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
    [Route("api/PersonMessage")]
    public class PersonMessageController : BaseApiController
    {
        /// <summary>
        /// 获取用户的所有消息
        /// </summary>
        /// <param name="id">用户GUID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IEnumerable<MessageModel> Get(string id)
        {
            return hostelContext.Messages.Where(d => d.To == id).OrderByDescending(d => d.CreateTime);
        }


    }
}
