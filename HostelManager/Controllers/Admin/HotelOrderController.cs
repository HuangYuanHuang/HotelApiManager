using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using Microsoft.EntityFrameworkCore;
using HostelService;
using HostelManager.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostelManager.Controllers.Admin
{
    public class HotelOrderController : BaseController<HotelWorkOrderModel>
    {
        protected override DbSet<HotelWorkOrderModel> dbSet => hostelContext.HotelOrders;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new HotelWorkOrderModel());
        }

        [HttpPost]
        public JsonResult Orders()
        {
            ContextService<HotelWorkOrderModel> service = new ContextService<HotelWorkOrderModel>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true).Select(d => new { id = d.Id, text = d.Title });
            return Json(list);
        }

        [HttpPost]
        public JsonResult OrderDetail(string id, string preTime)
        {
            DateTime pre = DateTime.Parse(preTime);
            var list = hostelContext.HotelOrders.Where(d => d.Hotel.GUID == id).OrderByDescending(d => d.CreateTime).Select(d => new HotelOrderDetaiModel()
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

            return Json(list);
        }

        [HttpPost]
        public JsonResult AreaOrders(int id)
        {
            IQueryable<HotelWorkOrderModel> list = null;
            if (id != 0)
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
            return Json(result);
        }



    }
}
