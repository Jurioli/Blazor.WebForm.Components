using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.GridView), typeof(GridViewEditRowStyle))]
    public partial class EditRowStyle
    {
        protected class GridViewEditRowStyle : PropertyComponentAdapter<EditRowStyle, System.Web.UI.WebControls.GridView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.EditRowStyle.Apply(() => parameters);
                this.Owner.EditRowStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
