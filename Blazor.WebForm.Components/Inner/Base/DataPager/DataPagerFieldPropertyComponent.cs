using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class DataPagerFieldPropertyComponent : PropertyComponent<System.Web.UI.WebControls.DataPager>
    {
        [Parameter]
        public string Visible { get; set; }
    }
}
