using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.GridView), typeof(GridViewInsertRowStyle))]
    public partial class InsertRowStyle
    {
        protected class GridViewInsertRowStyle : PropertyComponentAdapter<InsertRowStyle, System.Web.UI.WebControls.GridView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.InsertRowStyle.Apply(() => parameters);
                this.Owner.InsertRowStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
