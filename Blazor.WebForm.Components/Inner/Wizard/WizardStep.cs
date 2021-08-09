using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class WizardStep : WizardStepBaseControlPropertyComponent<System.Web.UI.WebControls.WizardStepCollection, System.Web.UI.WebControls.WizardStep>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(nameof(this.ChildContent)) && this.ChildContent != null)
            {
                System.Web.UI.RenderFragmentControl contentControl = new System.Web.UI.RenderFragmentControl();
                contentControl.Content = this.ChildContent;
                contentControl.Container = this.Control;
                this.Control.Controls.Add(contentControl);
            }
            this.Owner.Add(this.Control);
        }
    }
}
