using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public partial class PagerSettings : PropertyComponent<System.Web.UI.WebControls.DataBoundControl>, ICommonPropertyComponent
    {
        [Parameter]
        public string FirstPageImageUrl { get; set; }

        [Parameter]
        public string FirstPageText { get; set; }

        [Parameter]
        public string LastPageImageUrl { get; set; }

        [Parameter]
        public string LastPageText { get; set; }

        [Parameter]
        public System.Web.UI.WebControls.PagerButtons Mode { get; set; }

        [Parameter]
        public string NextPageImageUrl { get; set; }

        [Parameter]
        public string NextPageText { get; set; }

        [Parameter]
        public int PageButtonCount { get; set; }

        [Parameter]
        public System.Web.UI.WebControls.PagerPosition Position { get; set; }

        [Parameter]
        public string PreviousPageImageUrl { get; set; }

        [Parameter]
        public string PreviousPageText { get; set; }

        [Parameter]
        public bool Visible { get; set; }
    }
}
