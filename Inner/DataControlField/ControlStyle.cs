using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class ControlStyle : StylePropertyComponent<System.Web.UI.WebControls.DataControlField>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.ControlStyle.Apply(() => parameters);
            this.Owner.ControlStyle.Font.Apply(() => this.Font);
        }
    }
}
