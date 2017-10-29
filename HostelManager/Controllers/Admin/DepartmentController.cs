using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using Microsoft.EntityFrameworkCore;
using HostelService;

namespace HostelManager.Controllers.Admin
{
    public class DepartmentController :  BaseController<DepartmentModel>
    {
        protected override DbSet<DepartmentModel> dbSet => hostelContext.Departs;

        public IActionResult Index()
        {
            return View(new DepartmentModel());
        }

        [HttpPost]
       
        public JsonResult Departments()
        {
            ContextService<DepartmentModel> service = new ContextService<DepartmentModel>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true).Select(d => new { id = d.Id, text = d.DepartmentName });
            return Json(list);
        }
    }
}