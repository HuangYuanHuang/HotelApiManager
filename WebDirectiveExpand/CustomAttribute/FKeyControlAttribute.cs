using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FKeyControlAttribute: Attribute
    {
        public string ValueName { get; set; }

        public string PropName { get; set; }
    }
}
