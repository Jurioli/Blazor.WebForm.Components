using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class TableItemStylePropertyComponent<T> : StylePropertyComponent<T>
    {
        [Parameter]
        public System.Web.UI.WebControls.HorizontalAlign HorizontalAlign { get; set; }

        [Parameter]
        public System.Web.UI.WebControls.VerticalAlign VerticalAlign { get; set; }

        [Parameter]
        public bool Wrap { get; set; }
    }
}
