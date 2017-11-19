using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using HostelManager.Models;

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


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id">GUID</param>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public async Task<object> Put(string id, [FromBody]UpdatePersonModel model)
        {

            var obj = hostelContext.ServicePersons.FirstOrDefault(d => d.GUID == id);
            if (obj != null)
            {
                obj.Address = model.Address;
                obj.Phone = model.Phone;
                obj.RealName = model.RealName;
                obj.Sex = model.Sex;
                obj.IdentityCard = model.IdentityCard;
                try
                {
                   await hostelContext.SaveChangesAsync();
                    return new { state = true, message = "更新用户数据成功！" };
                }
                catch (Exception)
                {

                    return new { state = false, message = "数据保存失败，请确认数据是否完整！" };
                }
              
            }
            else
            {
                return new { state=false,message="无效的用户GUID"};
            }
        }

    }
}
