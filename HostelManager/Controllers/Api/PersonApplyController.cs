using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelManager.Models;
using HostelModel;
using HostelService;

namespace HostelManager.Controllers.Api
{
    /// <summary>
    /// 服务人员用工接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/PersonApply")]
    public class PersonApplyController : BaseApiController
    {
        /// <summary>
        /// 获取服务人员已经申请的用工列表
        /// </summary>
        /// <param name="id">服务人员guid</param>
        /// <returns></returns>
        // GET: api/PersonApply/5
        [HttpGet("{id}")]
        public IEnumerable<PersonOrderDetailModel> Get(string id)
        {
            var list = hostelContext.PersonOrders.Where(d => d.Person.GUID == id).Select(d => new PersonOrderDetailModel()
            {
                ApplyTime = d.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = d.Status,
                TotalApply = d.HotelOrder.Id,
                Order = new
                {
                    Id = d.HotelOrder.Id,
                    GUID = d.HotelOrder.GUID,
                    HotelGUID = d.HotelOrder.Hotel.GUID,
                    Start = d.HotelOrder.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                    End = d.HotelOrder.End.ToString("yyyy-MM-dd HH:mm:ss"),
                    Mark = d.HotelOrder.Mark,
                    HotelName = d.HotelOrder.Hotel.Name,
                    Num = d.HotelOrder.Num,
                    Billing = d.HotelOrder.Billing,
                    CreateTime = d.HotelOrder.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    ScheduleName = d.HotelOrder.Schedule.Name,
                    WorkTypeName = d.HotelOrder.WorkType.Name,
                    DepartMentName = d.HotelOrder.Department.DepartmentName,

                }
            }).ToList();

            list.ForEach(d =>
            {
                d.TotalApply = hostelContext.PersonOrders.Count(f => f.OrderId == d.TotalApply);
            });
            return list;
        }

        /// <summary>
        /// 申请工作
        /// </summary>
        /// <param name="model"></param>
        // POST: api/PersonApply
        [HttpPost]
        public object Post([FromBody]PersonApplyModel model)
        {
            try
            {
             
                if (!ModelState.IsValid)
                {
                    return new { state = false, message = "输入验证不合法" };
                }

                var result = hostelContext.ServicePersons.FirstOrDefault(d => d.Id == model.PersonId);
                if (result == null || result.Health == null || result.ICardBack == null || result.ICardPositive == null)
                {
                    return new { state = false, message = "用户资料未完整,请用户完善资料" };
                }
                //用户是否有其他工作未结束
                var count = hostelContext.PersonEmploys.Count(d => d.PersonId == model.PersonId && d.Status == 1);
                if (count > 0)
                {
                    return new { state = false, message = "用户善于工作未终止，请终止后再来申请新工作" };
                }
                hostelContext.PersonOrders.Add(new PersonOrderModel()
                {
                    OrderId = model.OrderId,
                    PersonId = model.PersonId,
                    Status = 1,
                    Mark = model.Mark
                });
                hostelContext.SaveChanges();

                return new { state = true, message = "操作成功" };
            }
            catch
            {
                return new { state = false, message = "数据操作服务器错误，请确认数据是否完整" };


            }
        }


    }
}
