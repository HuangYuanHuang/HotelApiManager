using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelManager.Models;

namespace HostelManager.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/HotelTodayGrab")]
    public class HotelTodayGrabController : BaseApiController
    {

        /// <summary>
        /// 获取酒店今日（抢单类型）人员列表
        /// </summary>
        /// <param name="id">酒店GUID</param>
        /// <returns></returns>
        // GET: api/HotelTodayGrab/5
        [HttpGet("{id}")]
        public IEnumerable<HotelGrabGroupModel> Get(string id)
        {

            var todayOrders = hostelContext.HotelOrders.Where(d => d.OrderType == 1 && d.Hotel.GUID == id && d.Start == DateTime.Now.Date).Select(d => new HotelTodayGrabModel
            {
                DepartMentName = d.Department.DepartmentName,
                OrderId = d.Id,
                OrderGUID = d.GUID,
                RoomNum = d.Num,
                Start = d.Start.ToString("yyyy-MM-dd")

            }).ToList();
            foreach (var item in todayOrders)
            {
                item.POrders = hostelContext.PersonOrders.Where(d => d.OrderId == item.OrderId).Select(d => new PersonOrderDetail()
                {
                    Person = d.Person,
                   
                    ApplyNum = d.ApplyNum ?? 0,
                    GUID = d.GUID,
                    
                    POrderId = d.Id,
                }).ToList();
                item.POrders.ForEach(d => d.GrabNum = hostelContext.GrabOrders.Where(f => f.POrderId == d.POrderId).Count());

            }
            return todayOrders.GroupBy(d => d.DepartMentName, (key, values) => new HotelGrabGroupModel { DepartMentName = key, Orders = values });

        }

    }
}
