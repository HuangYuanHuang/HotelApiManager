using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelManager.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/PersonEvaluate")]
    public class PersonEvaluateController : BaseApiController
    {      
        /// <summary>
        /// 获取用户被评论详情
        /// </summary>
        /// <param name="id">用户GUID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public object Get(string id)
        {
            var list = hostelContext.PersonEmploys.Join(hostelContext.ServicePersons.Where(d => d.GUID == id), d => d.PersonId, f => f.Id, (emploays, order) => new
            {
                Evaluate = emploays.Evaluate,
                Comment = emploays.Comment,
                CommentTime = emploays.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),

            });
            var total = list.Count(d => d.Evaluate != null);
            float? average = null;
            if (total > 0)
            {
                average = (float)(list.Sum(d => d.Evaluate) * 1.0 / total);
            }
            return new
            {
                Total = total,
                Average = average,
                ApplyNum = list.Count(),
                Details = list.OrderByDescending(d => d.CommentTime)
            };
        }
        
      
    }
}
