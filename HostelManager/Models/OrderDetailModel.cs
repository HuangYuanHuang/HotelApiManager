using HostelModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class OrderDetailModel
    {
        public int TotalApply { get; set; }

        public IEnumerable<PersonOrderDetail> Persons { get; set; }
    }

    public class PersonOrderDetail
    {
        public int Status { get; set; }

        public int ApplyNum { get; set; }
        public string GUID { get; set; }
        public string StatusStr { get { return Status == 1 ? "待处理" : Status == 2 ? "预录用" : Status == 3 ? "录用" : "拒绝"; } set { } }

        public int POrderId { get; set; }
        [JsonIgnore]
        public int? OrderType { get; set; }

        [JsonIgnore]
        public DateTime OrderStart { get; set; }

        public bool IsOffline { get; set; }
        public string ApplyTime { get; set; }

        /// <summary>
        /// 人员评分
        /// </summary>
        public float Evaluate { set; get; }
        /// <summary>
        /// 实际打扫房间数量
        /// </summary>
        public int GrabNum { get; set; }
        public ServicePersonModel Person { get; set; }
    }
}
