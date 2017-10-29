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
    public class WorkTypeController : BaseController<WorkTypeModel>
    {
        protected override DbSet<WorkTypeModel> dbSet => hostelContext.WorkTypes;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new WorkTypeModel());
        }


        [HttpPost]
        public JsonResult WorkTypes()
        {
            ContextService<WorkTypeModel> service = new ContextService<WorkTypeModel>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true).Select(d => new { id = d.Id, text = d.Name });
            return Json(list);
        }
    }
}
