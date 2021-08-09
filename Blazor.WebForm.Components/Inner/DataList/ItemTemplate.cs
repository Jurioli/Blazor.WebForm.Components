using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.DataList), typeof(ItemTemplate<>.DataListItemTemplate))]
    public partial class ItemTemplate<TItem>
    {
        protected class DataListItemTemplate : BindableTemplatePropertyComponentAdapter<ItemTemplate<TItem>, System.Web.UI.WebControls.DataList>
        {

        }
    }
}
