using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class NumericPagerField : DataPagerFieldPropertyComponent
    {
        [Parameter]
        public string PreviousPageImageUrl { get; set; }

        [Parameter]
        public string NumericButtonCssClass { get; set; }

        [Parameter]
        public string NextPreviousButtonCssClass { get; set; }

        [Parameter]
        public string NextPageText { get; set; }

        [Parameter]
        public string NextPageImageUrl { get; set; }

        [Parameter]
        public string CurrentPageLabelCssClass { get; set; }

        [Parameter]
        public string PreviousPageText { get; set; }

        [Parameter]
        public System.Web.UI.WebControls.ButtonType ButtonType { get; set; }

        [Parameter]
        public int ButtonCount { get; set; }

        [Parameter]
        public bool RenderNonBreakingSpacesBetweenControls { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            System.Web.UI.WebControls.NumericPagerField field = new System.Web.UI.WebControls.NumericPagerField();
            field.Apply(() => parameters);
            this.Owner.Fields.Add(field);
        }
    }
}
