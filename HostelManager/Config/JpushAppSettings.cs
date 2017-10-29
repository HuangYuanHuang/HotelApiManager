using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageWeb.Config
{
    /// <summary>
    /// 极光配置
    /// </summary>
    public class JpushAppSettings
    {
        /// <summary>
        /// APPKey
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// Secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Base64编码(appKey:Secret)
        /// </summary>
        public string Base64 { get; set; }

        /// <summary>
        /// 极光短信验证码配置
        /// </summary>
        public JPushSMS SmsCode { get; set; }

        /// <summary>
        /// 极光通知类短信配置
        /// </summary>
        public NoticeMessage SmsMessage { get; set; }
    }
    /// <summary>
    /// 极光短信配置
    /// </summary>
    public class JPushSMS
    {
        /// <summary>
        /// POST 发送短信URL地址
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// 短信模板ID
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 验证短信验证码地址
        /// </summary>
        public string VailUrl { get; set; }
    }

    /// <summary>
    /// 极光通知类短信配置
    /// </summary>
    public class NoticeMessage
    {
        /// <summary>
        /// Url
        /// </summary>
        public string ApiUrl { get; set; }
    }
}
