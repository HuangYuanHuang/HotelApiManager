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
    /// <summary>
    /// 酒店录用接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/HotelEmploy")]
    public class HotelEmployController : BaseApiController
    {

        /// <summary>
        /// 获取酒店录用人员列表
        /// </summary>
        /// <param name="id">酒店GUID</param>
        /// <returns></returns>
        // GET: api/HotelEmploy/5
        [HttpGet("{id}")]
        public IEnumerable<object> Get(string id)
        {
            return hostelContext.PersonEmploys.Where(d => d.HoterlOrder.Hotel.GUID == id).GroupBy(d => d.HoterlOrder.Department.DepartmentName,
                  (key, values) => new
                  {
                      DepartmentName = key,
                      Employs = values.OrderByDescending(d => d.CreateTime)
                  });
        }


        /// <summary>
        /// 酒店终止用户工作并评价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public object Put(string id, [FromBody]HotelComment model)
        {
            var obj = hostelContext.PersonEmploys.FirstOrDefault(d => d.GUID == id);
            if (obj != null)
            {
                obj.Status = 0;
                obj.Evaluate = model.Evaluate;
                obj.Comment = model.Comment;
            }
            try
            {
                hostelContext.SaveChanges();
                return new { state = true, message = "操作成功" };
            }
            catch (Exception)
            {

                return new { state = false, message = "数据操作服务器错误，请确认数据是否完整" };
            }
        }

    }
}
