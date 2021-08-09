using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class TableFooterRow : TableRowControlPropertyComponent<System.Web.UI.WebControls.Table, System.Web.UI.WebControls.TableFooterRow>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(nameof(this.ChildContent)) && this.ChildContent != null)
            {
                this.RenderWithCascading(this.Control.Cells, this.ChildContent);
            }
        }

        protected internal override void SetInnerPropertyWithCascading(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.Rows.Add(this.Control);
        }
    }
}
