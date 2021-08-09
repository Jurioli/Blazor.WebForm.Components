using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class SelectorStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.Calendar>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.SelectorStyle.Apply(() => parameters);
            this.Owner.SelectorStyle.Font.Apply(() => this.Font);
        }
    }
}
