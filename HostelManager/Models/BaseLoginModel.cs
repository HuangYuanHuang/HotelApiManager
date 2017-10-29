using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    public class BaseLoginModel
    {
        /// <summary>
        /// 国际移动设备识别码
        /// </summary>
        public string IMEI { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// 软件版本
        /// </summary>
        public string SoftVersion  { get; set; }

        /// <summary>
        /// 系统类型 （Android,IOS）
        /// </summary>
        public string SystemType  { get; set; }

        /// <summary>
        /// 账号类型 （用工端,酒店端，中介端）默认用工端
        /// </summary>
        public string AccoutType { get; set; } = "用工端";
    }
}
