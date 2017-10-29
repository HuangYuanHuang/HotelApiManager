using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebDirectiveExpand.Controls.Model;

namespace WebDirectiveExpand.Controls
{
    public class PageHeaderBuilder : IElementToHtml
    {
        public TagBuilder header;
        List<GroupBtnModel> btnGroup = new List<GroupBtnModel>()
        {
            new GroupBtnModel(){ NgClick="Model.Edit.newItem()",BtnTitle="新增",ClassName="btn-primary",Glyphicon="glyphicon-plus"},
            new GroupBtnModel(){NgClick="Model.Edit.editItem()", BtnTitle="编辑",ClassName="btn-success",Glyphicon="glyphicon-pencil"},
            new GroupBtnModel(){ NgClick="Model.Edit.removeItem()",BtnTitle="删除",ClassName="btn-danger",Glyphicon="glyphicon-remove"},
            new GroupBtnModel(){NgClick="Model.Edit.export()" ,BtnTitle="导出",ClassName="btn-info",Glyphicon="glyphicon-export"},
        };
        public PageHeaderBuilder(string title, string icon,bool isBtn = true)
        {
            header = new TagBuilder("div");
            //header.InnerHtml.AppendHtml($"<h1 class=\"text-light\">{title}<span class=\"{icon}\"></span></h1>");
            header.InnerHtml.AppendHtml($"<h3>{title}<span class=\"{icon}\"></span></h3><hr style='margin-top:0px;margin-bottom:10px'/>");
            if (isBtn)
                BuilderGroupBtn();
        }

        void BuilderGroupBtn()
        {
            TagBuilder p = new TagBuilder("p");
            foreach (var item in btnGroup)
            {
                p.InnerHtml.AppendHtml(item.ToTagBuilder());
            }
            header.InnerHtml.AppendHtml(p);
            header.InnerHtml.AppendHtml($"<hr style='margin-top:0px;margin-bottom:0px'/>");
        }
        public TagBuilder ToHtmlString()
        {
            return header;
        }
    }
}
