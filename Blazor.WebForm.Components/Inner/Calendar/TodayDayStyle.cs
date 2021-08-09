using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class TodayDayStyle : TableItemStylePropertyComponent<System.Web.UI.WebControls.Calendar>
    {
        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.TodayDayStyle.Apply(() => parameters);
            this.Owner.TodayDayStyle.Font.Apply(() => this.Font);
        }
    }
}
