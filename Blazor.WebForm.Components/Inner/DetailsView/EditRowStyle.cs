using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DetailsView), typeof(DetailsViewEditRowStyle))]
    public partial class EditRowStyle
    {
        protected class DetailsViewEditRowStyle : PropertyComponentAdapter<EditRowStyle, System.Web.UI.WebControls.DetailsView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.EditRowStyle.Apply(() => parameters);
                this.Owner.EditRowStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
