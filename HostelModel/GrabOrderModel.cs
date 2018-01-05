using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HostelModel
{
    [Table("T_Hostel_GrabOrder")]
    public class GrabOrderModel : BaseModel
    {
        public int PersonId { get; set; }

        public int OrderId { get; set; }
        public int POrderId { get; set; }

        public string RoomID { get; set; }

        public int RommStatus { get; set; }

        public int RoomIndex { get; set; }

    }
}
