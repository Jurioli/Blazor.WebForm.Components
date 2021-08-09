using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class TitleStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.Calendar>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.TitleStyle.Apply(() => parameters);
            this.Owner.TitleStyle.Font.Apply(() => this.Font);
        }
    }
}
