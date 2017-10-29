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
    public class HotelController : BaseController<HotelModel>
    {
        protected override DbSet<HotelModel> dbSet => hostelContext.Hotels;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new HotelModel());
        }

        [HttpPost]
        public JsonResult Hotels()
        {
            ContextService<HotelModel> service = new ContextService<HotelModel>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true).Select(d => new { id = d.Id, text = d.Name });
            return Json(list);
        }

        [HttpPost]
        public JsonResult Login(LoginHotelModel login)
        {
            var model = hostelContext.Accouts.FirstOrDefault(d => d.LoginName == login.username);
            if (model == null || login.password != "123")
            {
                return Json(new { state = false, message = "账号密码错误!" });
            }
            try
            {
                var hotelModel = hostelContext.Hotels.Include(d => d.Area).FirstOrDefault(d => d.Id == model.HotelId);
                return Json(new { state = true, message = "登录成功", token = Guid.NewGuid().ToString("N"), data = hotelModel });
            }
            catch (Exception)
            {

                return Json(new { state = false, message = "系统错误，未找到该账户管理的酒店信息，请联系管理员处理" });
            }


        }


        [HttpPost]
        public JsonResult HotelDetail(string id)
        {
            var res = hostelContext.Hotels.Include(d => d.Area).FirstOrDefault(d => d.GUID == id);
            return Json(new { state = true, message = "查询成功", data = res });
        }
    }
}
