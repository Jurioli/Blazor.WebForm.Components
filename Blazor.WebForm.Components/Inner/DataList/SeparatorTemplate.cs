using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DataList), typeof(DataListSeparatorTemplate))]
    public partial class SeparatorTemplate
    {
        protected class DataListSeparatorTemplate : TemplatePropertyComponentAdapter<SeparatorTemplate, System.Web.UI.WebControls.DataList>
        {

        }
    }
}
