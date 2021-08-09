using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DetailsView), typeof(DetailsViewEmptyDataRowStyle))]
    public partial class EmptyDataRowStyle
    {
        protected class DetailsViewEmptyDataRowStyle : PropertyComponentAdapter<EmptyDataRowStyle, System.Web.UI.WebControls.DetailsView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.EmptyDataRowStyle.Apply(() => parameters);
                this.Owner.EmptyDataRowStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
