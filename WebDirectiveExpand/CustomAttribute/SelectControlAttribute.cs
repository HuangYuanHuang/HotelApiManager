using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectControlAttribute: Attribute
    {
        public ItemDataType Type { get; set; }

        public string ItemsName { get; set; }

        /// <summary>
        /// 获取数据绝对URL
        /// </summary>
        public string Url { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
    }

    public enum ItemDataType
    {
        StaticItems,
        AbsUrl,
        ControllerAction

    }
}
