﻿using System;
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
    /// 区域用工列表接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/AreaWork")]
    public class AreaWorkController : BaseApiController
    {

        /// <summary>
        /// 获取区域的酒店用工列表
        /// </summary>
        /// <param name="id">区域id id为null 或者为0 则显示所有区域信息</param>
        /// <returns></returns>
        // GET: api/HotelWorkOrder/5
        [HttpGet("{id}")]
        public IEnumerable<object> Get(int? id)
        {
            IQueryable<HotelWorkOrderModel> list = null;
            if (id != null && id != 0)
            {
                list = hostelContext.HotelOrders.Where(d => d.Hotel.AreaId == id && d.Status == 1);
            }
            else
            {
                list = hostelContext.HotelOrders.Where(d => d.Status == 1);
            }
            var res = list.Select(d => new HotelAreaOrderModel()
            {
                Sort = d.Hotel.Sort,
                AreaId = d.Hotel.AreaId,
                AreaName = d.Hotel.Area.Name,
                HotelId = d.HotelId,
                HotelGUID = d.Hotel.GUID,
                Billing = d.Billing,
                DepartMentName = d.Department.DepartmentName,
                End = d.End.ToString("yyyy-MM-dd HH:mm:ss"),
                GUID = d.GUID,
                HotelName = d.Hotel.Name,
                Id = d.Id,
                KeyWord = d.KeyWord,
                Mark = d.Mark,
                Num = d.Num,
                ScheduleName = d.Schedule.Name,
                Start = d.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateTime = d.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                WorkTypeName = d.WorkType.Name

            }).OrderBy(d => d.Sort).ToList();

            foreach (var item in res)
            {
                item.AppliedNum = hostelContext.PersonOrders.Count(d => d.OrderId == item.Id);
            }
            var result = res.GroupBy(d => new { d.HotelId, d.HotelName, d.AreaId, d.AreaName, d.HotelGUID }, (key, values) => new
            {
                HotelGUID = key.HotelGUID,
                HotelId = key.HotelId,
                HotelName = key.HotelName,
                AreaId = key.AreaId,
                AreaName = key.AreaName,
                Works = values.OrderByDescending(d => d.CreateTime)
            });
            return result;
        }


    }
}