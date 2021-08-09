using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class EditItemStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.DataList>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.EditItemStyle.Apply(() => parameters);
            this.Owner.EditItemStyle.Font.Apply(() => this.Font);
        }
    }
}
