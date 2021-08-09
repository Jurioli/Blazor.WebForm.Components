using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class PolygonHotSpot : HotSpotPropertyComponent<System.Web.UI.WebControls.PolygonHotSpot>
    {
        [Parameter]
        public int Coordinates { get; set; }
    }
}
