using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DataList), typeof(AlternatingItemTemplate<>.DataListAlternatingItemTemplate))]
    public partial class AlternatingItemTemplate<TItem>
    {
        protected class DataListAlternatingItemTemplate : BindableTemplatePropertyComponentAdapter<AlternatingItemTemplate<TItem>, System.Web.UI.WebControls.DataList>
        {

        }
    }
}
