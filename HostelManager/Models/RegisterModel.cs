using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelManager.Models
{
    /// <summary>
    /// 用户注册模型
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// 真实姓名 Required
        /// </summary>
        public string RealName { set; get; }
        /// <summary>
        /// 性别 男,女 Required
        /// </summary>

        public string Sex { get; set; } = "女";

        /// <summary>
        /// 密码 
        /// </summary>

        public string Pwd { get; set; }

        /// <summary>
        /// 身份证 Required
        /// </summary>
        public string IdentityCard { set; get; }

        /// <summary>
        /// 手机号码 Required
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 验证码ID
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}
