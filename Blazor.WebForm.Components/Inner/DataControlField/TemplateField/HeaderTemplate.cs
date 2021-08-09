using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.TemplateField), typeof(TemplateFieldHeaderTemplate))]
    public partial class HeaderTemplate
    {
        protected class TemplateFieldHeaderTemplate : TemplatePropertyComponentAdapter<HeaderTemplate, System.Web.UI.WebControls.TemplateField>
        {

        }
    }
}
