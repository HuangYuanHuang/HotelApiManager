using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WebDirectiveExpand.Controls.Model;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebDirectiveExpand.CustomAttribute;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebDirectiveExpand.Controls
{
    public class BootstrapTableBuilder : IElementToHtml
    {

        ModelExpression viewModel;
        private Type modelType;
        string controller;
        TagBuilder table;
        List<ThreadTagModel> columns;


        public BootstrapTableBuilder(ModelExpression model, string controller)
        {
            this.modelType = model.ModelExplorer.Model.GetType();

            this.controller = controller;
            this.viewModel = model;
            table = new TagBuilder("table");
            table.AddCssClass("table table-hover");
            table.Attributes.Add("id", "builder_table");
            table.Attributes.Add("data-click-to-select", "true");
            table.Attributes.Add("data-toggle", "table");
            table.Attributes.Add("data-method", "post");
            table.Attributes.Add("data-show-refresh", "true");
            table.Attributes.Add("data-pagination", "true");
            table.Attributes.Add("data-show-columns", "true");
            table.Attributes.Add("data-height", "500px");
            table.Attributes.Add("data-search", "true");
            table.Attributes.Add("data-url", controller + "/List");
            columns = new List<ThreadTagModel>();

        }
        private void BuildHead()
        {
            TagBuilder thead = new TagBuilder("thead");
            table.InnerHtml.AppendHtml(thead);

            TagBuilder tr = new TagBuilder("tr");
            thead.InnerHtml.AppendHtml(tr);
            foreach (var prop in modelType.GetProperties())
            {
                var tableIgnore = prop.GetCustomAttribute<TableIgnoreAttribute>();
                if (tableIgnore != null)
                {
                    continue;
                }

                var displayAttribute = prop.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute == null)
                {
                    continue;
                }

                var th = new TagBuilder("th");
                var jsonProerty = prop.GetCustomAttribute<JsonPropertyAttribute>();
                string propName = jsonProerty == null ? prop.Name : jsonProerty.PropertyName;
                th.Attributes.Add("data-field", propName);
                th.InnerHtml.AppendHtml(displayAttribute.Name);
                columns.Add(new ThreadTagModel()
                {
                    Builder = th,
                    Sort = displayAttribute.Order

                });
            }

            tr.InnerHtml.AppendHtml("<th data-radio='true'></th>");
            columns.OrderBy(d => d.Sort).ToList().ForEach(d =>
            {
                tr.InnerHtml.AppendHtml(d.Builder);
            });

        }
        public TagBuilder ToHtmlString()
        {
            BuildHead();
            return table;
        }
    }
}
