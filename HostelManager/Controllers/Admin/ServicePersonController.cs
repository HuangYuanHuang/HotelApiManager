using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using Microsoft.EntityFrameworkCore;
using HostelService;
using HostelManager.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostelManager.Controllers.Admin
{
    public class ServicePersonController : BaseController<ServicePersonModel>
    {
        protected override DbSet<ServicePersonModel> dbSet => hostelContext.ServicePersons;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new ServicePersonModel());
        }

        [HttpPost]
        public JsonResult Persons()
        {
            ContextService<ServicePersonModel> service = new ContextService<ServicePersonModel>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true).Select(d => new { id = d.Id, text = d.RealName });
            return Json(list);
        }


        /// <summary>
        /// 获取服务人员已经申请的用工列表
        /// </summary>
        /// <param name="id">服务人员GUID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Orders(string id)
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
            return Json(list);
        }


        [HttpPost]
        public JsonResult Login(LoginPersonModel login)
        {
            var model = hostelContext.ServicePersons.FirstOrDefault(d => d.Phone == login.phone);
            if (model == null || login.password != "123")
            {
                return Json(new { state = false, message = "账号密码错误!" });
            }
            try
            {

                return Json(new { state = true, message = "登录成功", token = Guid.NewGuid().ToString("N"), data = model });
            }
            catch (Exception)
            {

                return Json(new { state = false, message = "系统错误，未找到账户信息，请联系管理员处理" });
            }


        }


        public JsonResult UpdatePwd(UpdatePersonPwdModel model)
        {
            if (model == null || model.oldPassword != "123")
            {
                return Json(new { state = false, message = "原密码错误!" });
            }

            return Json(new { state = true, message = "修改成功" });
        }


        [HttpPost]
        public JsonResult Upload([FromServices] IHostingEnvironment env, UploadImageModel model)
        {
            var result = hostelContext.ServicePersons.FirstOrDefault(d => d.GUID == model.GUID);
            if (result == null)
            {
                return Json(new { state = false, message = "未找到该人员信息，信息错误" });
            }
            string base64 =(model.data);
            string fileName = Path.Combine($"upload/{model.GUID}");
            try
            {
                byte[] arr2 = Convert.FromBase64String(base64.Split(',')[1]);
                string path = Path.Combine(env.WebRootPath, fileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileName += $"/{model.type}.jpg";
                System.IO.File.WriteAllText($"{path}/{model.type}_{DateTime.Now.ToFileTime()}.txt", base64);
                System.IO.File.WriteAllBytes($"{path}/{model.type}.jpg", arr2);
                if (model.type == "ICardPositive")
                {
                    result.ICardPositive = fileName;
                }
                else if (model.type == "ICardBack")
                {
                    result.ICardBack = fileName;
                }
                else
                {
                    result.Health = fileName;
                }
                hostelContext.SaveChanges();
            }
            catch (Exception ex)
            {

                return Json(new { state = false, message = ex.Message });
            }
          
            return Json(new { state = true, message = "图片上传成功" });
        }
    }
}
