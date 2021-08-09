using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.FormView), typeof(FormViewFooterStyle))]
    public partial class FooterStyle
    {
        protected class FormViewFooterStyle : PropertyComponentAdapter<FooterStyle, System.Web.UI.WebControls.FormView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.FooterStyle.Apply(() => parameters);
                this.Owner.FooterStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
