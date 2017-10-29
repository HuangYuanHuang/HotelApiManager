using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using Microsoft.EntityFrameworkCore;
using HostelManager.Models;
using HostelService;

namespace HostelManager.Controllers.Admin
{
    public class PersonOrderController : BaseController<PersonOrderModel>
    {
        protected override DbSet<PersonOrderModel> dbSet => hostelContext.PersonOrders;

        public IActionResult Index()
        {
            return View(new PersonOrderModel());
        }

        [HttpPost]
        public JsonResult Persons(int id)
        {
            var list = hostelContext.PersonOrders.Where(d => d.OrderId == id).Select(d => new PersonOrderDetail() { Person = d.Person, Status = d.Status, ApplyTime = d.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), GUID = d.GUID }).ToList();
            var result = new OrderDetailModel { TotalApply = list.Count, Persons = list };
            return Json(result);
        }
        /// <summary>
        /// 用工人员申请酒店用工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Apply(PersonOrderModel model)
        {
            try
            {
                ModelState.Remove("GUID");
                model.GUID = Guid.NewGuid().ToString("N");
                if (!ModelState.IsValid)
                {
                    return Json(new { state = false, message = "输入验证不合法" });
                }

                var result = hostelContext.ServicePersons.FirstOrDefault(d => d.Id == model.PersonId);
                if (result == null || result.Health == null || result.ICardBack == null || result.ICardPositive == null)
                {
                    return Json(new { state = false, message = "用户资料未完整,请用户完善资料" });
                }
                ContextService<PersonOrderModel> service = new ContextService<PersonOrderModel>(dbSet, hostelContext);
                service.AddEntity(model);

                return Json(new { state = true, message = "操作成功" });
            }
            catch
            {
                return Json(new { state = false, message = "数据操作服务器错误，请确认数据是否完整" });


            }
        }
    }
}