using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelKeyAttribute: Attribute
    {
        public ModelKeyAttribute()
        {

        }
    }
}
