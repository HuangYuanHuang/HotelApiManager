using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.Controls.Model
{
    public class FormGroupModel
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public TagBuilder Label { get; set; }

        public TagBuilder Input { set; get; }

        public TagBuilder TextDanger { set; get; }

        public int Sort { get; set; }
    }
}
