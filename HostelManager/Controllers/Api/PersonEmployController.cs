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
    /// 用户录用接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/PersonEmploy")]
    public class PersonEmployController : BaseApiController
    {


        /// <summary>
        /// 获取用户在酒店录用记录
        /// </summary>
        /// <param name="id">用户GUID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IEnumerable<PersonEmployModel> Get(string id)
        {
            return hostelContext.PersonEmploys.Where(d => d.Person.GUID == id).OrderByDescending(d => d.CreateTime);
        }



        /// <summary>
        /// 用户终止并评价酒店工作
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public object Put(string id, [FromBody]PersonComment model)
        {
            var obj = hostelContext.PersonEmploys.FirstOrDefault(d => d.GUID == id);
            if (obj != null)
            {
                obj.Status = 0;
                obj.HotelComment = model.HotelComment;
                obj.HotelEvaluate = model.HotelEvaluate;
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
