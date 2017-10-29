using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    /// <summary>
    /// 用户登录模型
    /// </summary>
    public class UserLoginModel:BaseLoginModel
    {
        /// <summary>
        /// 验证码ID
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
    }
}
