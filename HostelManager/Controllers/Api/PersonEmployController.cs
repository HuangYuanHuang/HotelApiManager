using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using HostelManager.Models;
using Microsoft.EntityFrameworkCore;
namespace HostelManager.Controllers.Api
{
    /// <summary>
    /// 用户录用接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/PersonEmploy")]
    public class PersonEmployController : BaseApiController
    {


        /// <summary>
        /// 获取用户在酒店录用记录
        /// </summary>
        /// <param name="id">用户GUID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IEnumerable<object> Get(string id)
        {
            return hostelContext.PersonEmploys.Where(d => d.Person.GUID == id).OrderByDescending(d => d.CreateTime).Select(d => new
            {
                PersonId = d.PersonId,
                GUID = d.GUID,
                CreateTime = d.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Order = new
                {
                    HotelName = d.HoterlOrder.Hotel.Name,
                    DepartName = d.HoterlOrder.Department.DepartmentName,
                    ScheduleName = d.HoterlOrder.Schedule.Name,
                    WorkTypeName = d.HoterlOrder.WorkType.Name,
                    Num = d.HoterlOrder.Num,

                    Start = d.HoterlOrder.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                    End = d.HoterlOrder.End.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreateTime = d.HoterlOrder.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Billing = d.HoterlOrder.Billing,
                },
                HotelOrderId = d.HotelOrderId,
                Status = d.Status,

                Evaluate = d.Evaluate,
                HotelEvaluate = d.HotelEvaluate,
                Comment = d.Comment,
                HotelComment = d.HotelComment,
            });
        }



        /// <summary>
        /// 用户终止并评价酒店工作
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public object Put(string id, [FromBody]PersonComment model)
        {
            var obj = hostelContext.PersonEmploys.FirstOrDefault(d => d.GUID == id);
            if (obj != null)
            {
                obj.Status = 0;
                obj.HotelComment = model.HotelComment;
                obj.HotelEvaluate = model.HotelEvaluate;
            }
            try
            {
                hostelContext.PersonOrders.RemoveRange(hostelContext.PersonOrders.Where(d => d.PersonId == obj.PersonId));

                var person = hostelContext.ServicePersons.FirstOrDefault(d => d.Id == obj.PersonId);
                var order = hostelContext.HotelOrders.Where(d => d.Id == obj.HotelOrderId).Select(d => new
                {
                    HotelName = d.Hotel.Name,
                    HotelGUID = d.Hotel.GUID,
                    HotelDepartment = d.Department.DepartmentName,
                    HotelWork = d.WorkType.Name
                }).FirstOrDefault();
                hostelContext.Messages.Add(new HostelModel.MessageModel()
                {
                    Context = $"{person.RealName}与{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}《终止了》您酒店录用的{order?.HotelDepartment}-{order?.HotelWork}的工作！",
                    From = person.GUID,
                    To = order?.HotelGUID,
                    Type = "工作终止"
                });
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
