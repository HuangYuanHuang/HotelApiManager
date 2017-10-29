using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using WebDirectiveExpand.Controls.Model;
using System.Linq;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebDirectiveExpand.CustomAttribute;


namespace WebDirectiveExpand.Controls
{


    public class FormBuilder : IElementToHtml
    {
        ModelExpression viewModel;
        TagBuilder main = new TagBuilder("div");
        TagBuilder boday = new TagBuilder("div");
        bool formColumn = true;
        IHtmlGenerator Generator;
        ViewContext viewContext;
        Type modelType;
        public FormBuilder(ModelExpression model, IHtmlGenerator generator, ViewContext viewContext, string size, bool column)
        {
            this.modelType = model.ModelExplorer.Model.GetType();
            this.viewContext = viewContext;
            this.viewModel = model;
            this.formColumn = column;
            this.Generator = generator;
            main.AddCssClass("modal fade");
            main.Attributes.Add("id", "myModal");
            main.Attributes.Add("tabindex", "-1");
            main.Attributes.Add("role", "dialog");
            main.Attributes.Add("aria-labelledby", "myModalLabel");

            TagBuilder document = new TagBuilder("div");
            document.AddCssClass("modal-dialog " + size);
            document.Attributes.Add("role", "document");

            main.InnerHtml.AppendHtml(document);

            TagBuilder modalContext = new TagBuilder("div");
            modalContext.AddCssClass("modal-content");
            document.InnerHtml.AppendHtml(modalContext);

            TagBuilder modalHeader = new TagBuilder("div");
            modalHeader.AddCssClass("modal-header");
            modalHeader.InnerHtml.AppendHtml("<button type = \"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>");
            modalHeader.InnerHtml.AppendHtml("<h4 class=\"modal-title\" id=\"myModalLabel\">{{Model.Edit.modalTitle}}</h4>");
            modalContext.InnerHtml.AppendHtml(modalHeader);

            boday.AddCssClass("modal-body");
            modalContext.InnerHtml.AppendHtml(boday);

            modalContext.InnerHtml.AppendHtml("<div class=\"modal-footer\"><button type =\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">关闭</button>");
            modalContext.InnerHtml.AppendHtml("<button type =\"button\" class=\"btn btn-primary\" ng-click='Model.Edit.saveChanges()'>保存更改</button></div>");

            BuilderForm();
        }


        private void BuilderForm()
        {


            var form = new TagBuilder("form");

            boday.InnerHtml.AppendHtml(form);

            form.AddCssClass("form-horizontal");
            form.Attributes.Add("id", "builder_form");
            int lableWidth = formColumn ? 2 : 2;
            int divWidth = formColumn ? 4 : 10;

            int groupIndex = formColumn ? 2 : 1;
            int index = 0;
            TagBuilder group = null;
            List<FormGroupModel> listGroup = new List<FormGroupModel>();
            foreach (var prop in modelType.GetProperties())
            {
                var formIgnore = prop.GetCustomAttribute<FormIgnoreAttribute>();
                if (formIgnore != null)
                {
                    continue;
                }
                var displayAttribute = prop.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute == null || displayAttribute.GetAutoGenerateField() == false || prop.GetCustomAttribute<ModelKeyAttribute>() != null)
                {
                    continue;
                }
                var modelExplorer = viewModel.ModelExplorer.GetExplorerForProperty(prop.Name);
                var control = new FormGroupModel()
                {

                    Sort = displayAttribute.Order,
                    DisplayName = displayAttribute.Name?.Length < 1 ? prop.Name : displayAttribute.Name,
                    Name = prop.Name
                };
                listGroup.Add(control);
                BuildInputType(prop, control, modelExplorer);

                if (control.Input != null)
                {
                    control.Input.Attributes.Add("ng-model", $"Model.Active.{prop.Name}");
                }
            }

            foreach (var item in listGroup.OrderBy(d => d.Sort))
            {
                if (index++ % groupIndex == 0 || group == null)
                {
                    group = new TagBuilder("div");
                    form.InnerHtml.AppendHtml(group);
                    group.AddCssClass("form-group");
                }
                var label = new TagBuilder("label");
                label.AddCssClass($"col-sm-{lableWidth} control-label");
                label.Attributes.Add("for", item.Name);
                label.InnerHtml.AppendHtml(item.DisplayName);
                group.InnerHtml.AppendHtml(label);

                var divInput = new TagBuilder("div");
                group.InnerHtml.AppendHtml(divInput);

                divInput.AddCssClass($"col-sm-{divWidth}");
                if (item.Input != null)
                {
                    divInput.InnerHtml.AppendHtml(item.Input);
                }

                if (item.TextDanger != null)
                {
                    divInput.InnerHtml.AppendHtml(item.TextDanger);
                }
            }


            var modelKey = modelType.GetProperties().Where(d => d.GetCustomAttribute<ModelKeyAttribute>() != null).FirstOrDefault();
            if (modelKey != null)
            {
                form.InnerHtml.AppendHtml($"<input type='text' id='{modelKey.Name}' name='{modelKey.Name}' value='{modelKey.GetValue(viewModel.Model, null)}' style='display:none' ng-model='Model.Active.{modelKey.Name}'>");

            }

            form.InnerHtml.AppendHtml("<div ng-show='Model.Edit.saveSuccess' class=\"alert alert-success\" role=\"alert\">操作成功!</div>");
            form.InnerHtml.AppendHtml("<div ng-show='Model.Edit.saveError' class=\"alert alert-danger\" role=\"alert\">{{Model.Edit.errorInfo}}</div>");

        }

        void BuildInputType(PropertyInfo prop, FormGroupModel group, ModelExplorer modelExplorer)
        {
            //判断是否有设置dataType
            var dataType = prop.GetCustomAttribute<DataTypeAttribute>();
            if (dataType?.DataType != null)
            {
                switch (dataType.DataType)
                {
                    case DataType.Text:
                        group.Input = Generator.GenerateTextBox(viewContext, modelExplorer, prop.Name, null, null, new { @class = "form-control" });
                        group.TextDanger = Generator.GenerateValidationMessage(viewContext, modelExplorer, prop.Name, null, viewContext.ValidationMessageElement, new { @class = "text-danger" });
                        break;
                    case DataType.Password:
                        group.Input = Generator.GeneratePassword(viewContext, modelExplorer, prop.Name, null, new { @class = "form-control" });
                        group.TextDanger = Generator.GenerateValidationMessage(viewContext, modelExplorer, prop.Name, null, viewContext.ValidationMessageElement, new { @class = "text-danger" });
                        break;
                    case DataType.PhoneNumber:
                        group.Input = Generator.GenerateTextBox(viewContext, modelExplorer, prop.Name, null, null, new { @class = "form-control", @type = "tel" });
                        group.TextDanger = Generator.GenerateValidationMessage(viewContext, modelExplorer, prop.Name, null, viewContext.ValidationMessageElement, new { @class = "text-danger" });
                        break;
                    case DataType.MultilineText:
                        group.Input = Generator.GenerateTextArea(viewContext, modelExplorer, prop.Name, 2, 1, new { @class = "form-control" });
                        group.TextDanger = Generator.GenerateValidationMessage(viewContext, modelExplorer, prop.Name, null, viewContext.ValidationMessageElement, new { @class = "text-danger" });
                        break;
                    case DataType.EmailAddress:
                        group.Input = Generator.GenerateTextBox(viewContext, modelExplorer, prop.Name, null, null, new { @class = "form-control", @type = "email" });
                        group.TextDanger = Generator.GenerateValidationMessage(viewContext, modelExplorer, prop.Name, null, viewContext.ValidationMessageElement, new { @class = "text-danger" });
                        break;
                    case DataType.Url:
                        group.Input = Generator.GenerateTextBox(viewContext, modelExplorer, prop.Name, null, null, new { @class = "form-control", @type = "url" });
                        group.TextDanger = Generator.GenerateValidationMessage(viewContext, modelExplorer, prop.Name, null, viewContext.ValidationMessageElement, new { @class = "text-danger" });
                        break;
                    default:
                        break;
                }
                return;
            }

            // 自定义selectControl控件
            var selectControl = prop.GetCustomAttribute<SelectControlAttribute>();
            if (selectControl != null)
            {
                if (selectControl.Type == ItemDataType.StaticItems)
                {
                    List<SelectListItem> nodes = new List<SelectListItem>();
                    var items = modelType.GetProperty(selectControl.ItemsName);
                    if (items != null)
                    {
                        nodes = items.GetValue(viewModel.Model, null) as List<SelectListItem>;
                    }
                    group.Input = Generator.GenerateSelect(viewContext, modelExplorer, null, prop.Name, nodes?.ToList(), false, new { @class = "form-control" });
                    var initValue = nodes.FirstOrDefault(d => d.Selected)?.Value;
                    var ngInit = initValue?.Length > 0 ? initValue : nodes[0].Value;
                    group.Input.Attributes.Add("ng-init", $"Model.Active.{prop.Name}='{ngInit}'");

                }
                else
                {
                    string url = selectControl.Type == ItemDataType.AbsUrl ? selectControl.Url : $"http://{viewContext.HttpContext.Request.Host.Value}/{selectControl.Controller}/{selectControl.Action}";
                    group.Input = new TagBuilder("select-control");
                    group.Input.AddCssClass("form-control");
                    group.Input.Attributes.Add("id", prop.Name);
                    group.Input.Attributes.Add("name", prop.Name);
                    group.Input.Attributes.Add("request-url", url);
                    group.Input.Attributes.Add("request-init", $"Model.Active.{prop.Name}");
                }

                return;
            }
            //自定义 radiolist 控件
            //var radioListControl = prop.GetCustomAttribute<RadioListAttribute>();
            //if (radioListControl != null)
            //{
            //    var values = radioListControl.Values.Split(',');
            //    var tests = radioListControl.Texts.Split(',');

            //    if (values.Length == tests.Length)
            //    {
            //        group.Input = new TagBuilder("div");
            //        var index = 0;
            //        foreach (var item in values)
            //        {
            //            group.Input = new TagBuilder("label");
            //            group.Input.InnerHtml.AppendHtml($"<input type='radio' name='{prop.Name}' ng-model='Model.Active.{item}' class='form-control'/>{tests[index++]}");
                       
            //        }

            //    }
               
            //    return;
            //}

            if (prop.GetType() == typeof(int) || prop.GetType() == typeof(float) || prop.GetType() == typeof(double))
            {
                group.Input = Generator.GenerateTextBox(viewContext, modelExplorer, prop.Name, null, null, new { @class = "form-control", @type = "number" });

            }
            else
            {
                group.Input = Generator.GenerateTextBox(viewContext, modelExplorer, prop.Name, null, null, new { @class = "form-control" });
            }
            group.TextDanger = Generator.GenerateValidationMessage(viewContext, modelExplorer, prop.Name, null, viewContext.ValidationMessageElement, new { @class = "text-danger" });

        }
        public TagBuilder ToHtmlString()
        {
            return main;
        }
    }
}
