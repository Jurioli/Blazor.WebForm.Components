using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class WizardStepBaseControlPropertyComponent<T, TControl> : ViewControlPropertyComponent<T, TControl>
        where TControl : System.Web.UI.WebControls.WizardStepBase, new()
    {
        [Parameter]
        public bool AllowReturn
        {
            get
            {
                return this.Control.AllowReturn;
            }
            set
            {
                this.Control.AllowReturn = value;
            }
        }

        [Parameter]
        public System.Web.UI.WebControls.WizardStepType StepType
        {
            get
            {
                return this.Control.StepType;
            }
            set
            {
                this.Control.StepType = value;
            }
        }

        [Parameter]
        public string Title
        {
            get
            {
                return this.Control.Title;
            }
            set
            {
                this.Control.Title = value;
            }
        }
    }
}
