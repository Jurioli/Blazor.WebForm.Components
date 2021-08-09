using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.FormView), typeof(FormViewRowStyle))]
    public partial class RowStyle
    {
        protected class FormViewRowStyle : PropertyComponentAdapter<RowStyle, System.Web.UI.WebControls.FormView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.RowStyle.Apply(() => parameters);
                this.Owner.RowStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}