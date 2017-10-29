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
        /// 部门ID
        /// </summary>
        [Required]
        public int DepartID { get; set; }

        /// <summary>
        /// 排班ID
        /// </summary>
        [Required]
        public int ScheduleId { get; set; }

        /// <summary>
        /// 工种ID
        /// </summary>
        [Required]
        public int WorkTypeId { get; set; }

        /// <summary>
        /// 需求人数
        /// </summary>
        [Required]
        public int Num { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
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
    }
}
