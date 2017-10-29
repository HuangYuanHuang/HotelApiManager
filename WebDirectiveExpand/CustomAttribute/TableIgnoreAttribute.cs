using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableIgnoreAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FormIgnoreAttribute : Attribute
    {
    }
}
