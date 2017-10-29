using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace WebDirectiveExpand.Controls
{

    [HtmlTargetElement("crud-form")]
    public class GridBuilder : TagHelper, IGridBuilder
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

       

        protected IHtmlGenerator Generator { get; }

        [HtmlAttributeName("asp-url")]
        public string Url { get; set; }

        [HtmlAttributeName("asp-title")]
        public string Title { set; get; }

        [HtmlAttributeName("asp-icon")]
        public string Icon { get; set; }

        [HtmlAttributeName("asp-header-btn")]
        public bool BtnGroup { set; get; } = true;

        [HtmlAttributeName("asp-modal-size")]
        //modal-lg  modal-sm
        public string ModalSize { set; get; } = "";

        [HtmlAttributeName("asp-form-double")]
        public bool DoubleColumn { set; get; } = true;

        [HtmlAttributeName("ng-controller")]
        public string ngController { set; get; } = "mainController";

        public GridBuilder(IHtmlGenerator generator)
        {
            Generator = generator;
            

        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("ng-controller", ngController);
            var headerBuilder = new PageHeaderBuilder(Title, Icon, BtnGroup).ToHtmlString();
            output.Content.AppendHtml(headerBuilder);

            if (For.Model != null)
            {
                var bootsrtapTable = new BootstrapTableBuilder(For, Url).ToHtmlString();
                output.Content.AppendHtml(bootsrtapTable);

                var modalBuilder = new FormBuilder(For, Generator, ViewContext, ModalSize, DoubleColumn).ToHtmlString();
                output.Content.AppendHtml(modalBuilder);
            }
           

            output.Content.AppendHtml(await output.GetChildContentAsync());

        }
    }
}
