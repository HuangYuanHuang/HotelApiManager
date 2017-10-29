using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDirectiveExpand.Controls
{
    public interface IGridBuilder
    {
    }

    public interface IElementToHtml
    {
        TagBuilder ToHtmlString();

    }
}
