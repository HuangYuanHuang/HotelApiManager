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
                OrderId = d.HotelOrder.Id,

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
                d.RoomNum = hostelContext.PersonOrders.FirstOrDefault(f => f.OrderId == d.OrderId)?.ApplyNum ?? 0;
                d.EmployNum = hostelContext.PersonEmploys.Count(f => f.HotelOrderId == d.OrderId && f.Status == 1);
                d.TotalApply = hostelContext.PersonOrders.Count(f => f.OrderId == d.OrderId);
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
                var order = hostelContext.HotelOrders.Where(d => d.Id == model.OrderId).Select(d => new
                {
                    HotelName = d.Hotel.Name,
                    HotelGUID = d.Hotel.GUID,
                    HotelDepartment = d.Department.DepartmentName,
                    HotelWork = d.WorkType.Name,
                    Num = d.Num,
                    Max = d.Max,
                    OrderType = d.OrderType,
                }).FirstOrDefault();

                var result = hostelContext.ServicePersons.FirstOrDefault(d => d.Id == model.PersonId);
                if (result == null || result.Health == null || result.ICardBack == null || result.ICardPositive == null || result.RealName == null)
                {
                    return new { state = false, message = "用户资料未完整,请用户完善资料", code = 4001 };
                }
                //用户是否有其他工作未结束
                var count = hostelContext.PersonEmploys.Count(d => d.PersonId == model.PersonId && d.Status == 1);
                if (count > 0 && order.OrderType != 1)
                {
                    return new { state = false, message = "用户尚有工作未终止，请终止后再来申请新工作", code = 4002 };
                }
                if (order.OrderType == 1)
                {
                    int applyNum = hostelContext.PersonOrders.Where(d => d.OrderId == model.OrderId).Sum(d => d.ApplyNum) ?? 0;
                    if (applyNum + model.Num > order.Num)
                    {
                        model.Num = order.Num - applyNum;
                    }
                    if (model.Num < 5)
                    {
                        return new { state = false, message = "房间数不足", code = 4005 };

                    }
                    var personOrder = hostelContext.PersonOrders.FirstOrDefault(d => d.OrderId == model.OrderId && d.PersonId == model.PersonId);
                    if (personOrder == null)
                    {
                        personOrder = new PersonOrderModel()
                        {
                            OrderId = model.OrderId,
                            PersonId = model.PersonId,
                            Status = 1,
                            ApplyNum = 0,
                            Mark = model.Mark
                        };
                        hostelContext.PersonOrders.Add(personOrder);
                    }
                    personOrder.ApplyNum += model.Num;
                    if (personOrder.ApplyNum > order.Max)
                    {
                        return new { state = false, message = "已超过单人申请上限", code = 4006 };
                    }
                }
                else
                {
                    hostelContext.PersonOrders.Add(new PersonOrderModel()
                    {
                        OrderId = model.OrderId,
                        PersonId = model.PersonId,
                        Status = 1,
                        ApplyNum = model.Num,
                        Mark = model.Mark
                    });
                }


                hostelContext.Messages.Add(new HostelModel.MessageModel()
                {
                    Context = $"{result.RealName} 于 {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 申请 了您酒店发布的{order?.HotelDepartment}-{order?.HotelWork}的工作，请及时处理！",
                    From = result.GUID,
                    To = order?.HotelGUID,
                    Type = "工作申请"
                });
                hostelContext.SaveChanges();

                return new { state = true, message = "操作成功" };
            }
            catch
            {
                return new { state = false, message = "数据操作服务器错误，请确认数据是否完整", code = 5000 };


            }
        }


    }
}
