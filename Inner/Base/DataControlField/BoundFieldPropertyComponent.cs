using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class BoundFieldPropertyComponent<TField> : DataControlFieldPropertyComponent<TField>
        where TField : System.Web.UI.WebControls.BoundField, new()
    {
        [Parameter]
        public string NullDisplayText { get; set; }

        [Parameter]
        public bool HtmlEncodeFormatString { get; set; }

        [Parameter]
        public bool HtmlEncode { get; set; }

        [Parameter]
        public string DataFormatString { get; set; }

        [Parameter]
        public string DataField { get; set; }

        [Parameter]
        public bool ConvertEmptyStringToNull { get; set; }

        [Parameter]
        public bool ApplyFormatInEditMode { get; set; }

        [Parameter]
        public System.Web.UI.ValidateRequestMode ValidateRequestMode { get; set; }

        [Parameter]
        public bool ReadOnly { get; set; }
    }
}
