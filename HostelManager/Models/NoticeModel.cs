using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageWeb.Models
{
    /// <summary>
    /// 模板通知对象
    /// </summary>
    public class NoticeModel
    {
        /// <summary>
        /// 发送手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 酒店名称 
        /// </summary>
        public string Hotel { get; set; }

        /// <summary>
        /// 录用状态 预录用 录用 解聘
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 信息生成时间
        /// </summary>
        public string Time => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 信息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 极光通知类模板数据
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            if (Type == "解聘") //解聘模板 JPush
                return $"{{\"mobile\":\"{Phone}\",\"temp_id\":145077,\"temp_para\":{{\"hotel\":\"{Hotel}\"}}}}";
            if (Type == "预录用")
                return $"{{\"mobile\":\"{Phone}\",\"temp_id\":145076,\"temp_para\":{{\"date\":\"{Time}\",\"title\":\"{Title}\"}}}}";
            return $"{{\"mobile\":\"{Phone}\",\"temp_id\":145075,\"temp_para\":{{\"date\":\"{Time}\",\"title\":\"{Title}\"}}}}";
        }
    }
}
