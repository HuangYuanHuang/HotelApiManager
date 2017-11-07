﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelManager.Models;

namespace HostelManager.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/HotelOrderPerson")]
    public class HotelOrderPersonController : BaseApiController
    {
        /// <summary>
        /// 获取酒店用工人员申请列表
        /// </summary>
        /// <param name="id">OrderID</param>
        /// <returns></returns>
        // GET: api/HotelOrderPerson/5
        [HttpGet("{id}")]
        public OrderDetailModel Get(int id)
        {
            var list = hostelContext.PersonOrders.Where(d => d.OrderId == id).Select(d => new PersonOrderDetail()
            {
                Person = d.Person,
                Status = d.Status,

                ApplyTime = d.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                GUID = d.GUID
            }).ToList();
            foreach (var item in list)
            {
                var count = hostelContext.PersonEmploys.Count(d => d.Evaluate != null);
                if (count > 0)
                {
                    item.Evaluate = (float)(hostelContext.PersonEmploys.Sum(d => d.Evaluate) * 1.0 / count);
                }

            }
            var result = new OrderDetailModel { TotalApply = list.Count, Persons = list };
            return result;
        }


    }
}