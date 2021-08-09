using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.ListView), typeof(AlternatingItemTemplate<>.ListViewAlternatingItemTemplate))]
    public partial class AlternatingItemTemplate<TItem>
    {
        protected class ListViewAlternatingItemTemplate : BindableTemplatePropertyComponentAdapter<AlternatingItemTemplate<TItem>, System.Web.UI.WebControls.ListView>
        {

        }
    }
}
