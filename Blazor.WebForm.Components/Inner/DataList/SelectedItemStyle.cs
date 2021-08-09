using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class SelectedItemStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.DataList>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.SelectedItemStyle.Apply(() => parameters);
            this.Owner.SelectedItemStyle.Font.Apply(() => this.Font);
        }
    }
}
