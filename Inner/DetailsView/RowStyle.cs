using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DetailsView), typeof(DetailsViewRowStyle))]
    public partial class RowStyle
    {
        protected class DetailsViewRowStyle : PropertyComponentAdapter<RowStyle, System.Web.UI.WebControls.DetailsView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.RowStyle.Apply(() => parameters);
                this.Owner.RowStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}