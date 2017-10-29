using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    /// <summary>
    /// 酒店用工查询对象
    /// </summary>
    public class HotelOrderQueryModel
    {
        /// <summary>
        /// 酒店GUID
        /// </summary>
        [Required]
        public string HotelGUID { get; set; }


        /// <summary>
        /// 上次刷新时间
        /// </summary>
        [Required]
        public DateTime PreTime { get; set; }
    }
}
