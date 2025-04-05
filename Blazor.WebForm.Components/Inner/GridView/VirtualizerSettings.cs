using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.GridView), typeof(GridViewVirtualizerSettings))]
    public partial class VirtualizerSettings
    {
        protected class GridViewVirtualizerSettings : PropertyComponentAdapter<VirtualizerSettings, System.Web.UI.WebControls.GridView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.VirtualizerSettings.Apply(() => parameters);
            }
        }
    }
}
