using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public partial class VirtualizerSettings : PropertyComponent<System.Web.UI.WebControls.DataBoundControl>, ICommonPropertyComponent
    {
        [Parameter]
        public string SpacerElement { get; set; }

        [Parameter]
        public float? ItemSize { get; set; }

        [Parameter]
        public int? OverscanCount { get; set; }

        [Parameter]
        public int? MaxItemCount { get; set; }
    }
}
