using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.Wizard), typeof(WizardLayoutTemplate))]
    public partial class LayoutTemplate
    {
        protected class WizardLayoutTemplate : TemplatePropertyComponentAdapter<LayoutTemplate, System.Web.UI.WebControls.Wizard>
        {

        }
    }
}
