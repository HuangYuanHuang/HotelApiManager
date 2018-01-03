using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    /// <summary>
    /// 房间状态更新模型
    /// </summary>
    public class GrabOrderViewModel
    {
        /// <summary>
        /// 房间状态
        /// </summary>
        public int RommStatus { get; set; }


        /// <summary>
        /// 房间ID
        /// </summary>
        public int Id { get; set; }
    }
}
