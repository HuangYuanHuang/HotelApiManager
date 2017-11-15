using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    /// <summary>
    /// 酒店上下线订单查询对象
    /// </summary>
    public class HotelOrderOnlineModel
    {
        /// <summary>
        /// 酒店GUID
        /// </summary>
        [Required]
        public string HotelGUID { get; set; }


        /// <summary>
        /// 订单状态  0-待审核, 1-审核通过 ,2-审核未通过,3-上线,4-下线 
        /// </summary>
        [Required]
        public int Status { get; set; }
    }
}
