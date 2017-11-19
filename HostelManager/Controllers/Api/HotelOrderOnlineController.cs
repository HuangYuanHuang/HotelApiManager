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
    [Route("api/HotelOrderOnline")]
    public class HotelOrderOnlineController : BaseApiController
    {


        /// <summary>
        /// 酒店订单状态查询
        /// </summary>
        /// <param name="model">查询对象</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<HotelOrderDetaiModel> Get([FromQuery]HotelOrderOnlineModel model)
        {
            var list = hostelContext.HotelOrders.Where(d => d.Hotel.GUID == model.HotelGUID && d.Status == model.Status).OrderByDescending(d => d.CreateTime).Select(d => new HotelOrderDetaiModel()
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
                item.EmployNum = hostelContext.PersonEmploys.Count(d => d.HotelOrderId == item.Id&&d.Status==1);
                item.AppliedNum = hostelContext.PersonOrders.Count(d => d.OrderId == item.Id);

            }
            return list;
        }



        /// <summary>
        /// 酒店订单上下线 （订单只有通过审核才能上下线操作）
        /// </summary>
        /// <param name="id">订单GUID</param>
        /// <param name="value">1-上线，0-下线</param>
        [HttpPut("{id}")]
        public object Put(string id, [FromBody]int value)
        {
            if (!(value == 1 || value == 0))
            {
                return new { state = false, message = "非法操作" };
            }
            var obj = hostelContext.HotelOrders.FirstOrDefault(d => d.GUID == id);
            if (obj != null)
            {
                if (value == 1)
                {
                    if (obj.Status == 1 || obj.Status == 4 || obj.Status == 3)
                    {
                        obj.Status = 3;
                        obj.CreateTime = DateTime.Now;
                    }
                    else
                    {
                        return new { state = false, message = "该订单为审核，无法上线，请等待中介后台审核！" };
                    }
                }
                else
                {
                    if (obj.Status == 3)
                    {
                        obj.Status = 4;
                    }
                    else
                    {
                        return new { state = false, message = "该订单未上线，无需进行该操作！" };
                    }
                }
                try
                {
                    hostelContext.SaveChanges();
                    return new { state = true, message = "操作成功！" };
                }
                catch (Exception)
                {

                    return new { state = false, message = "服务器错误,数据保存失败！" };
                }

            }
            else
            {
                return new { state = false, message = "非法的ID" };
            }

        }


    }
}
