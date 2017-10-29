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
    public class AreaController : BaseController<AreaModel>
    {
        protected override DbSet<AreaModel> dbSet { get => hostelContext.Areas; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new AreaModel());
        }

        [HttpPost]
        public JsonResult Areas()
        {
            ContextService<AreaModel> service = new ContextService<AreaModel>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true).Select(d=>new { id=d.Id,text=d.Name});
            return Json(list);
        }
    }
}
