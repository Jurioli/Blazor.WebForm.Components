using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class AlternatingItemStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.DataList>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.AlternatingItemStyle.Apply(() => parameters);
            this.Owner.AlternatingItemStyle.Font.Apply(() => this.Font);
        }
    }
}
