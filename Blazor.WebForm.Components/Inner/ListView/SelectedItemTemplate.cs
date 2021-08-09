using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.ListView), typeof(SelectedItemTemplate<>.ListViewSelectedItemTemplate))]
    public partial class SelectedItemTemplate<TItem>
    {
        protected class ListViewSelectedItemTemplate : BindableTemplatePropertyComponentAdapter<SelectedItemTemplate<TItem>, System.Web.UI.WebControls.ListView>
        {

        }
    }
}
