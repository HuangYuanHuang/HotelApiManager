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

     
    }
}