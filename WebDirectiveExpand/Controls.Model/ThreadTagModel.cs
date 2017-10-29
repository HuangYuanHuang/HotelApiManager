using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.Controls.Model
{
    /// <summary>
    /// Bootstrap Table 标题对象
    /// </summary>
    public class ThreadTagModel
    {
        /// <summary>
        /// 标题排序索引
        /// </summary>
        public int Sort { get; set; }

        public TagBuilder Builder { get; set; }
    }
}
