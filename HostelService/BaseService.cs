using System;
using System.Collections.Generic;
using System.Text;

namespace HostelService
{
    public class BaseService:IDisposable
    {
        private HostelModel.HostelContext hostelContext;
        public BaseService()
        {
            hostelContext = new HostelModel.HostelContext();
        }

        public void Dispose()
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
