using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.Controls.Model
{
    /// <summary>
    /// 页面操作按钮对象
    /// </summary>
    public class GroupBtnModel
    {
        public string ClassName { get; set; }

        public string Size { get; set; } = "btn";

        public string BtnTitle { get; set; }

        public string Glyphicon { get; set; } = "";

        public string NgClick { get; set; }
        public TagBuilder ToTagBuilder()
        {
            
            //<button type="button" class="btn btn-default" aria-label="Left Align">
            //< span class="glyphicon glyphicon-align-left" aria-hidden="true"></span>
            //</button>
            TagBuilder btn = new TagBuilder("button");
            btn.Attributes.Add("ng-click", NgClick);
            btn.Attributes.Add("type", "button");
            btn.Attributes.Add("style", "margin-right:10px");
            btn.AddCssClass($"{Size} {ClassName}");

            if (Glyphicon.Length > 2)
            {
                TagBuilder span = new TagBuilder("span");
                span.AddCssClass($"glyphicon {Glyphicon}");
                span.Attributes.Add("aria-hidden", "true");
                btn.InnerHtml.AppendHtml(span);
            }
            btn.InnerHtml.AppendHtml(" " + BtnTitle);
            return btn;
        }
    }
}
