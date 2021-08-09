using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DataList), typeof(DataListFooterTemplate))]
    public partial class FooterTemplate
    {
        protected class DataListFooterTemplate : TemplatePropertyComponentAdapter<FooterTemplate, System.Web.UI.WebControls.DataList>
        {

        }
    }
}
