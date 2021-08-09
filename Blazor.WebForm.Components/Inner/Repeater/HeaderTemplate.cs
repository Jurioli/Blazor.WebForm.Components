using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.Repeater), typeof(RepeaterHeaderTemplate))]
    public partial class HeaderTemplate
    {
        protected class RepeaterHeaderTemplate : TemplatePropertyComponentAdapter<HeaderTemplate, System.Web.UI.WebControls.Repeater>
        {

        }
    }
}
