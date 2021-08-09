using Blazor.WebForm.UI.PropertyComponents;
using Extensions.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(PluralHolder), typeof(HolderTemplate<>.PluralHolderHolderTemplate))]
    public partial class HolderTemplate<TItem>
    {
        protected class PluralHolderHolderTemplate : BindableTemplatePropertyComponentAdapter<HolderTemplate<TItem>, PluralHolder>
        {

        }
    }
}
