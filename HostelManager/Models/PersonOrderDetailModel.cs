using HostelModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class PersonOrderDetailModel
    {
        public int Status { get; set; }

        public string StatusStr { get { return Status == 1 ? "待处理" : Status == 2 ? "预录用" : Status == 3 ? "录用" : "拒绝"; } }


        /// <summary>
        /// 总共申请人
        /// </summary>
        public int TotalApply { get; set; }
        public String ApplyTime { get; set; }

        [JsonIgnore]
        public int OrderType { get; set; }

        public string GrabStatus { get; set; } = "工作中";
        [JsonIgnore]
        public DateTime Start { get; set; }

        public int EmployNum { get; set; }
        public int RoomNum { get; set; }
        public int OrderId { get; set; }
        public object Order { get; set; }
        public int POrderId { get; set; }
    }
}
