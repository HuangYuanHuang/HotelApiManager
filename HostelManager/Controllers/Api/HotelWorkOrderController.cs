using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using HostelManager.Models;
using MessageWeb.Config;
using Microsoft.Extensions.Options;
using HostelManager.Common;
using Microsoft.EntityFrameworkCore;

namespace HostelManager.Controllers.Api
{
    /// <summary>
    /// 酒店用工需求列表
    /// </summary>
    [Produces("application/json")]
    [Route("api/HotelWorkOrder")]
    public class HotelWorkOrderController : BaseApiController
    {

        private readonly IOptions<JpushAppSettings> options;
        /// <summary>
        /// 构造函数 通过IOC获取配置节点
        /// </summary>
        /// <param name="_options"></param>
        public HotelWorkOrderController(IOptions<JpushAppSettings> _options)
        {
            this.options = _options;
        }
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
                item.EmployNum = hostelContext.PersonEmploys.Count(d => d.HotelOrderId == item.Id);
                item.AppliedNum = hostelContext.PersonOrders.Count(d => d.OrderId == item.Id);
                item.NewApply = hostelContext.PersonOrders.Count(d => d.OrderId == item.Id && d.CreateTime > pre);
            }
            return list;
        }

        /// <summary>
        /// 酒店发布用工需求 新增账号等级发布权限控制
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public object Post([FromBody]HotelOrderItemModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return new { state = false, message = "输入验证不合法" };
                }
                var level = hostelContext.Accouts.FirstOrDefault(d => d.HotelId == model.HotelId);
                if (level == null)
                {
                    return new { state = false, message = "数据有误,无效的HotelId" };
                }
                if (level.Level == 1)
                {
                    var count = hostelContext.HotelOrders.Count(d => d.HotelId == model.HotelId);
                    if (count > 1)
                    {
                        return new { state = false, message = "该酒店账号为普通会员，只能免费发布两条用工信息" };
                    }
                }

                hostelContext.HotelOrders.Add(new HotelWorkOrderModel()
                {
                    Billing = model.Billing,
                    DepartID = model.DepartID,
                    End = model.End,
                    HotelId = model.HotelId,
                    Num = model.Num,
                    Mark = model.Mark,
                    ScheduleId = model.ScheduleId,
                    Start = model.Start,
                    WorkTypeId = model.WorkTypeId
                });
                hostelContext.SaveChanges();
                return new { state = true, message = "操作成功" };
            }
            catch
            {
                return new { state = false, message = "数据操作服务器错误，请确认数据是否完整" };


            }
        }

        /// <summary>
        /// 酒店更新用户申请状态 对于预录用，录用状态短信通知
        /// </summary>
        /// <param name="id">GUID</param>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public async Task<object> Put(string id, [FromBody]HotelOrderStatusModel model)
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
                    HotelOrderId = model.OrderId,
                    PersonId = model.PersonId,
                    Status = 1,

                });

            }
            if (obj != null)
            {
                obj.Status = model.Status;
                if (obj.Status == 2 || obj.Status == 3)
                {
                    NoticeCommon notice = new NoticeCommon(options);

                    var orderDatail = hostelContext.HotelOrders.Include(d => d.Hotel).Include(d => d.Department).Include(d => d.WorkType).FirstOrDefault(d => d.Id == model.OrderId);

                    var person = hostelContext.ServicePersons.FirstOrDefault(d => d.Id == obj.PersonId);
               
                    var order = hostelContext.HotelOrders.Where(d => d.Id == obj.OrderId).Select(d => new
                    {
                        HotelName = d.Hotel.Name,
                        HotelGUID = d.Hotel.GUID,
                        HotelDepartment = d.Department.DepartmentName,
                        HotelWork = d.WorkType.Name
                    }).FirstOrDefault();
                    string statsStr = obj.Status == 2 ? "预录用" : "录用";
                    hostelContext.Messages.Add(new HostelModel.MessageModel()
                    {
                        Context = $"{order?.HotelName}与{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}《{statsStr}》您的{order?.HotelDepartment}-{order?.HotelWork}工作！",
                        From = order?.HotelGUID,
                        To = person?.GUID,
                        Type = "工作状态变更"
                    });

                    await notice.SendNotice(new MessageWeb.Models.NoticeModel()
                    {
                        Type = statsStr,
                        Phone = hostelContext.ServicePersons.FirstOrDefault(d => d.Id == model.PersonId)?.Phone,
                        Title = $"{orderDatail?.HotelName}-{orderDatail?.DepartName}-{orderDatail?.WorkTypeName}"
                    });
                }
                //对于录用状态删除其余申请记录
                if (obj.Status == 3)
                {
                    hostelContext.PersonOrders.RemoveRange(hostelContext.PersonOrders.Where(d => d.Id != obj.Id && d.PersonId == obj.PersonId));
                }


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
