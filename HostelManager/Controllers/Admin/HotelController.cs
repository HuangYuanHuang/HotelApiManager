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
        public JsonResult HotelDetail(string id)
        {
            var res = hostelContext.Hotels.Include(d => d.Area).FirstOrDefault(d => d.GUID == id);
            return Json(new { state = true, message = "查询成功", data = res });
        }
    }
}
