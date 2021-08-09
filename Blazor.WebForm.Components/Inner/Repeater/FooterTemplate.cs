using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.Repeater), typeof(RepeaterFooterTemplate))]
    public partial class FooterTemplate
    {
        protected class RepeaterFooterTemplate : TemplatePropertyComponentAdapter<FooterTemplate, System.Web.UI.WebControls.Repeater>
        {

        }
    }
}
