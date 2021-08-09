using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.FormView), typeof(FormViewEditRowStyle))]
    public partial class EditRowStyle
    {
        protected class FormViewEditRowStyle : PropertyComponentAdapter<EditRowStyle, System.Web.UI.WebControls.FormView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.EditRowStyle.Apply(() => parameters);
                this.Owner.EditRowStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
