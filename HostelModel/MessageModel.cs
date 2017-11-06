using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HostelModel
{
    [Table("T_Hostel_Message")]
    public class MessageModel : BaseModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Type { get; set; }

        public string Context { get; set; }
    }
}
