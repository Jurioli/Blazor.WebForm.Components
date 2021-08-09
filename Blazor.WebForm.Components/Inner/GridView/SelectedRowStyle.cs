using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class SelectedRowStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.GridView>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.SelectedRowStyle.Apply(() => parameters);
            this.Owner.SelectedRowStyle.Font.Apply(() => this.Font);
        }
    }
}
