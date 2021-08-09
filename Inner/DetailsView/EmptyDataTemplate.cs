using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DetailsView), typeof(DetailsViewEmptyDataTemplate))]
    public partial class EmptyDataTemplate
    {
        protected class DetailsViewEmptyDataTemplate : TemplatePropertyComponentAdapter<EmptyDataTemplate, System.Web.UI.WebControls.DetailsView>
        {

        }
    }
}