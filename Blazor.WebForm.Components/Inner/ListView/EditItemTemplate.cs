using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.ListView), typeof(EditItemTemplate<>.ListViewEditItemTemplate))]
    public partial class EditItemTemplate<TItem>
    {
        protected class ListViewEditItemTemplate : BindableTemplatePropertyComponentAdapter<EditItemTemplate<TItem>, System.Web.UI.WebControls.ListView>
        {

        }
    }
}
