using Blazor.WebForm.UI.PropertyComponents;
using Extensions.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(HolderSource), typeof(HolderTemplate<>.HolderSourceHolderTemplate))]
    public partial class HolderTemplate<TItem>
    {
        protected class HolderSourceHolderTemplate : BindableTemplatePropertyComponentAdapter<HolderTemplate<TItem>, HolderSource>
        {

        }
    }
}
