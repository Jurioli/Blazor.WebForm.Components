using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class FieldHeaderStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.DetailsView>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.FieldHeaderStyle.Apply(() => parameters);
            this.Owner.FieldHeaderStyle.Font.Apply(() => this.Font);
        }
    }
}
