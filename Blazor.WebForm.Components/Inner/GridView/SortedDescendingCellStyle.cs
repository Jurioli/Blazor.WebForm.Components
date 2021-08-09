using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class SortedDescendingCellStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.GridView>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.SortedDescendingCellStyle.Apply(() => parameters);
            this.Owner.SortedDescendingCellStyle.Font.Apply(() => this.Font);
        }
    }
}
