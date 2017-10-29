using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GridAttribute : Attribute
    {
        public int Column { get;private set; }

        public string RootUrl { get;private set; }
        public GridAttribute(int column ,string absUrl)
        {
            this.Column = column;
            this.RootUrl = absUrl;
        }
    }
}
