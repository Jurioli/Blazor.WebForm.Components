using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.FormView), typeof(FormViewHeaderTemplate))]
    public partial class HeaderTemplate
    {
        protected class FormViewHeaderTemplate : TemplatePropertyComponentAdapter<HeaderTemplate, System.Web.UI.WebControls.FormView>
        {

        }
    }
}
