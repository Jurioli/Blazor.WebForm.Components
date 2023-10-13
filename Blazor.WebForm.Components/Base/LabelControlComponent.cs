using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class LabelControlComponent<TControl> : WebControlComponent<TControl>
        where TControl : Label, new()
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Text
        {
            get
            {
                return this.Control.Text;
            }
            set
            {
                this.Control.Text = value;
            }
        }

        protected override void SetInnerPropertyWithInner(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(nameof(this.ChildContent)) && this.ChildContent != null)
            {
                RenderUtility.SetContentString(this.Control, this.ChildContent);
            }
        }
    }
}
