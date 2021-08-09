using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.GridView), typeof(GridViewPagerTemplate))]
    public partial class PagerTemplate
    {
        protected class GridViewPagerTemplate : TemplatePropertyComponentAdapter<PagerTemplate, System.Web.UI.WebControls.GridView>
        {

        }
    }
}
