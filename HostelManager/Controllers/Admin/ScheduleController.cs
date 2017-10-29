using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using Microsoft.EntityFrameworkCore;
using HostelService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostelManager.Controllers.Admin
{
    public class ScheduleController : BaseController<ScheduleModel>
    {
        protected override DbSet<ScheduleModel> dbSet => hostelContext.Schedules;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new ScheduleModel());
        }

        [HttpPost]
        public JsonResult Schedules()
        {
            ContextService<ScheduleModel> service = new ContextService<ScheduleModel>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true).Select(d => new { id = d.Id, text = d.Name });
            return Json(list);
        }
    }
}
