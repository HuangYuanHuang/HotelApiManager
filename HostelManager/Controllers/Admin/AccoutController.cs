using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostelManager.Controllers.Admin
{
    public class AccoutController : BaseController<AccoutModel>
    {
        protected override DbSet<AccoutModel> dbSet => hostelContext.Accouts;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new AccoutModel());
        }
    }
}
