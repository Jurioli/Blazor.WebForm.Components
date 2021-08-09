using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class NextPreviousPagerField : DataPagerFieldPropertyComponent
    {
        [Parameter]
        public bool ShowLastPageButton { get; set; }

        [Parameter]
        public bool ShowFirstPageButton { get; set; }

        [Parameter]
        public bool RenderDisabledButtonsAsLabels { get; set; }

        [Parameter]
        public bool RenderNonBreakingSpacesBetweenControls { get; set; }

        [Parameter]
        public string PreviousPageText { get; set; }

        [Parameter]
        public string PreviousPageImageUrl { get; set; }

        [Parameter]
        public string NextPageText { get; set; }

        [Parameter]
        public string NextPageImageUrl { get; set; }

        [Parameter]
        public string LastPageText { get; set; }

        [Parameter]
        public bool ShowNextPageButton { get; set; }

        [Parameter]
        public string LastPageImageUrl { get; set; }

        [Parameter]
        public string FirstPageImageUrl { get; set; }

        [Parameter]
        public System.Web.UI.WebControls.ButtonType ButtonType { get; set; }

        [Parameter]
        public string ButtonCssClass { get; set; }

        [Parameter]
        public string FirstPageText { get; set; }

        [Parameter]
        public bool ShowPreviousPageButton { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            System.Web.UI.WebControls.NextPreviousPagerField field = new System.Web.UI.WebControls.NextPreviousPagerField();
            field.Apply(() => parameters);
            this.Owner.Fields.Add(field);
        }
    }
}
