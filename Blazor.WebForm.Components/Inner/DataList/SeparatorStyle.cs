using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class SeparatorStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.DataList>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.SeparatorStyle.Apply(() => parameters);
            this.Owner.SeparatorStyle.Font.Apply(() => this.Font);
        }
    }
}
