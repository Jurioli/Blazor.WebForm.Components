using Blazor.WebForm.UI;
using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class TemplatedWizardStep : WizardStepBaseControlPropertyComponent<System.Web.UI.WebControls.WizardStepCollection, System.Web.UI.WebControls.TemplatedWizardStep>
    {
        [Parameter]
        public RenderFragment ContentTemplate { get; set; }

        [Parameter]
        public RenderFragment CustomNavigationTemplate { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(nameof(this.ContentTemplate)))
            {
                this.Control.ContentTemplate = this.GetTemplateProperty(this.ContentTemplate);
            }
            if (parameters.ContainsKey(nameof(this.CustomNavigationTemplate)))
            {
                this.Control.CustomNavigationTemplate = this.GetTemplateProperty(this.CustomNavigationTemplate);
            }
            this.Owner.Add(this.Control);
        }
    }
}
