using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class StepStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.Wizard>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.StepStyle.Apply(() => parameters);
            this.Owner.StepStyle.Font.Apply(() => this.Font);
        }
    }
}
