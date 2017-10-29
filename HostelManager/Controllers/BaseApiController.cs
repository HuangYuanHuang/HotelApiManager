using HostelModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Controllers
{
    public class BaseApiController: Controller, IDisposable
    {
        protected HostelContext hostelContext;
        public BaseApiController()
        {
            hostelContext = new HostelContext();
        }

        
        
        protected override void Dispose(bool disposing)
        {
            if (hostelContext != null)
            {
                try
                {
                    hostelContext.Dispose();
                }
                catch (Exception)
                {

                   
                }
             
            }
        }
    }
}
