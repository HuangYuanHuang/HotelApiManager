using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RadioListAttribute: Attribute
    {
        /// <summary>
        /// value数组 以,分割
        /// </summary>
        public string Values { get; set; }

        /// <summary>
        /// text数组 以,分割
        /// </summary>
        public string Texts { get; set; }
    }

   
}
