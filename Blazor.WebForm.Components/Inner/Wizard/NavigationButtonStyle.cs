using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class NavigationButtonStyle : StylePropertyComponent<System.Web.UI.WebControls.Wizard>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.NavigationButtonStyle.Apply(() => parameters);
            this.Owner.NavigationButtonStyle.Font.Apply(() => this.Font);
        }
    }
}
