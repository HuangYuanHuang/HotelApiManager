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
    /// 酒店用工需求列表
    /// </summary>
    [Produces("application/json")]
    [Route("api/HotelWorkOrder")]
    public class HotelWorkOrderController : BaseApiController
    {
        /// <summary>
        /// 获取酒店用工发布列表
        /// </summary>
        /// <param name="model">查询对象</param>
        /// <returns>HotelOrderDetaiModel</returns>
        [HttpGet]
        public IEnumerable<HotelOrderDetaiModel> Get([FromQuery]HotelOrderQueryModel model)
        {
            DateTime pre = model.PreTime;
            var list = hostelContext.HotelOrders.Where(d => d.Hotel.GUID == model.HotelGUID).OrderByDescending(d => d.CreateTime).Select(d => new HotelOrderDetaiModel()
            {
                DepartID = d.DepartID,
                ScheduleId = d.ScheduleId,
                WorkTypeId = d.WorkTypeId,
                Billing = d.Billing,
                DepartMentName = d.Department.DepartmentName,
                End = d.End.ToString("yyyy-MM-dd HH:mm:ss"),
                GUID = d.GUID,
                Examine = d.Examine,
                Status = d.Status,
                HotelName = d.Hotel.Name,
                Id = d.Id,
                Mark = d.Mark,
                Num = d.Num,
                ScheduleName = d.Schedule.Name,
                Start = d.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateTime = d.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                WorkTypeName = d.WorkType.Name

            }).ToList();

            foreach (var item in list)
            {
                item.EmployNum = hostelContext.PersonOrders.Count(d => d.OrderId == item.Id && d.Status == 3);
                item.AppliedNum = hostelContext.PersonOrders.Count(d => d.OrderId == item.Id);
                item.NewApply = hostelContext.PersonOrders.Count(d => d.OrderId == item.Id && d.CreateTime > pre);
            }
            return list;
        }

        /// <summary>
        /// 酒店发布用工需求
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public object Post([FromBody]HotelWorkOrderModel model)
        {
            try
            {
                ModelState.Remove("GUID");
                model.GUID = Guid.NewGuid().ToString("N");
                if (!ModelState.IsValid)
                {
                    return new { state = false, message = "输入验证不合法" };
                }

                hostelContext.HotelOrders.Add(model);
                hostelContext.SaveChanges();
                return new { state = true, message = "操作成功" };
            }
            catch
            {
                return new { state = false, message = "数据操作服务器错误，请确认数据是否完整" };


            }
        }

        /// <summary>
        /// 酒店更新用户申请状态
        /// </summary>
        /// <param name="id">GUID</param>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public object Put(string id, [FromBody]HotelOrderStatusModel model)
        {
            var obj = hostelContext.PersonOrders.FirstOrDefault(d => d.GUID == id);
            if (model.Status == 3) //用户已经被录用后不能被其他酒店录用
            {
                var count = hostelContext.PersonEmploys.Count(d => d.PersonId == model.PersonId && d.Status == 1);
                if (count > 0)
                {
                    return new { state = false, message = "用户已经被其他酒店录用" };
                }
                hostelContext.PersonEmploys.Add(new PersonEmployModel()
                {
                    HotelOrderId=model.OrderId,
                    PersonId=model.PersonId,
                    Status=1,
                   
                });

            }
            if (obj != null)
            {
                obj.Status = model.Status;
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