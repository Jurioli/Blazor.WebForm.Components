using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DataControlField), typeof(DataControlFieldItemStyle))]
    public partial class ItemStyle
    {
        protected class DataControlFieldItemStyle : PropertyComponentAdapter<ItemStyle, System.Web.UI.WebControls.DataControlField>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.ItemStyle.Apply(() => parameters);
                this.Owner.ItemStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
