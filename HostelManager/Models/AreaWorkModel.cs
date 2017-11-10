using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class AreaWorkModel
    {
        public float HotelEvaluate { get; set; }
        public string HotelGUID { get; set; }

        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public int AreaId { get; set; }

        public string AreaName { get; set; }

        public IEnumerable<HotelAreaOrderModel> Works { get; set; }

    }
}
