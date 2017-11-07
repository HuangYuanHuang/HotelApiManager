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



    }
}
