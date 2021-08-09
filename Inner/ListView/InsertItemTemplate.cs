using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.ListView), typeof(InsertItemTemplate<>.ListViewInsertItemTemplate))]
    public partial class InsertItemTemplate<TItem>
    {
        protected class ListViewInsertItemTemplate : BindableTemplatePropertyComponentAdapter<InsertItemTemplate<TItem>, System.Web.UI.WebControls.ListView>
        {

        }
    }
}
