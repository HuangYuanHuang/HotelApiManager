using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    /// <summary>
    /// 用户申请状态变更
    /// </summary>
    public class HotelOrderStatusModel
    {
     
        public int OrderId { get; set; }


        public int PersonId { get; set; }

        /// <summary>
        /// 审核状态 1 ? "待处理" 2 ? "预录用"  3  "录用" :4: "拒绝";
        /// </summary>
        public int Status { get; set; }
    }
}
