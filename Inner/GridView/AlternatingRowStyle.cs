using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.GridView), typeof(GridViewAlternatingRowStyle))]
    public partial class AlternatingRowStyle
    {
        protected class GridViewAlternatingRowStyle : PropertyComponentAdapter<AlternatingRowStyle, System.Web.UI.WebControls.GridView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.AlternatingRowStyle.Apply(() => parameters);
                this.Owner.AlternatingRowStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
