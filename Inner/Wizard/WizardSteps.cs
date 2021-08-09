using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class WizardSteps : PropertyComponent<System.Web.UI.WebControls.Wizard>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(nameof(this.ChildContent)) && this.ChildContent != null)
            {
                this.RenderWithCascading(this.Owner.WizardSteps, this.ChildContent);
            }
        }
    }
}
