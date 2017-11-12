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
    [Route("api/PersonInfo")]
    public class PersonInfoController : BaseApiController
    {
        /// <summary>
        /// 获取服务人员基本信息
        /// </summary>
        /// <param name="id">服务人员GUID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ServicePersonModel Get(string id)
        {
            var model = hostelContext.ServicePersons.FirstOrDefault(d => d.GUID == id);
            return model;
        }

    }
}
