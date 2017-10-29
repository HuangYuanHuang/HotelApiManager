using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HostelModel
{
    [Table("T_Hostel_UserOnline")]    
    public class UserOnlineModel: BaseModel
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
        public string SoftVersion { get; set; }

        /// <summary>
        /// 系统类型 （Android,IOS）
        /// </summary>
        public string SystemType { get; set; }

        /// <summary>
        /// 账号类型 （用工端,酒店端，中介端）默认用工端
        /// </summary>
        public string AccoutType { get; set; } = "用工端";

        /// <summary>
        /// 用户GUID
        /// </summary>
        public string UserGUID { get; set; }

        /// <summary>
        /// Token
        /// </summary>

        public string Token { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// 登陆手机号
        /// </summary>
        public string Phone { get; set; }

    }
}
