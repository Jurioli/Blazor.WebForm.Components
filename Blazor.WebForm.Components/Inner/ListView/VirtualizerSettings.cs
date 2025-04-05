using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.ListView), typeof(ListViewVirtualizerSettings))]
    public partial class VirtualizerSettings
    {
        protected class ListViewVirtualizerSettings : PropertyComponentAdapter<VirtualizerSettings, System.Web.UI.WebControls.ListView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.VirtualizerSettings.Apply(() => parameters);
            }
        }
    }
}
