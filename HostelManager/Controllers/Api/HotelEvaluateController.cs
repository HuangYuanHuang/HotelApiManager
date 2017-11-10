using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelManager.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/HotelEvaluate")]
    public class HotelEvaluateController : BaseApiController
    {

        /// <summary>
        /// 获取酒店被评论详情
        /// </summary>
        /// <param name="id">酒店GUID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public object Get(string id)
        {
            var list = hostelContext.PersonEmploys.Join(hostelContext.HotelOrders.Where(d => d.Hotel.GUID == id), d => d.HotelOrderId, f => f.Id, (emploays, order) => new
            {
                HotelEvaluate = emploays.HotelEvaluate,
                HotelComment = emploays.HotelComment,
                CommentTime = emploays.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),

            });
            var total = list.Count(d => d.HotelEvaluate != null);
            float? average = null;
            if (total > 0)
            {
                average = (float)(list.Sum(d => d.HotelEvaluate) * 1.0 / total);
            }
            return new
            {
                Total = total,
                Average = average,
                OrderNum = list.Count(),
                Details = list.OrderByDescending(d => d.CommentTime)
            };

        }

    }
}
