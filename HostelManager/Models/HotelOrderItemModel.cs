using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    /// <summary>
    /// 酒店新增用工模型
    /// </summary>
    public class HotelOrderItemModel
    {
      
        /// <summary>
        /// 酒店ID
        /// </summary>
        [Required]
        public int HotelId { get; set; }

        /// <summary>
        /// 部门ID 默认客房部
        /// </summary>

        public int DepartID { get; set; } = 2;

        /// <summary>
        /// 排班ID 默认白班
        /// </summary>

        public int ScheduleId { get; set; } = 1;

        /// <summary>
        /// 工种ID 默认客服打扫
        /// </summary>

        public int WorkTypeId { get; set; } = 1;

        /// <summary>
        /// 需求数量（人数|房间数）
        /// </summary>
        [Required]
        public int Num { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// 结束时间 短期用工结束时间不需要传 自动开始时间加一天
        /// </summary>       
        public DateTime End { get; set; }

        /// <summary>
        /// 计费方式
        /// </summary>
        [Required]
        public string Billing { get; set; }

        /// <summary>
        /// 需求备注
        /// </summary>
        public string Mark { get; set; } = "无";

        /// <summary>
        /// 订单类型 0-长期用工（默认值） 1-短期用工（酒店房间抢单用工）
        /// </summary>
        public int OrderType { get; set; } = 0;


        /// <summary>
        /// 单人上限
        /// </summary>
        public int Min { get; set; } = 20;

        /// <summary>
        /// 单人下限
        /// </summary>
        public int Max { get; set; } = 5;
    }
}
