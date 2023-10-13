using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class ListItem : PropertyComponent<System.Web.UI.WebControls.ListControl>
    {
        [Parameter]
        public bool Enabled { get; set; }

        [Parameter]
        public bool Selected { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
            item.Apply(() => parameters);
            if (parameters.ContainsKey(nameof(this.ChildContent)) && this.ChildContent != null)
            {
                Blazor.WebForm.UI.RenderUtility.SetContentString(item, this.ChildContent);
            }
            this.Owner.Items.Add(item);
        }
    }
}
