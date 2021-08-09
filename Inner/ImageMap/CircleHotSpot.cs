using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class CircleHotSpot : HotSpotPropertyComponent<System.Web.UI.WebControls.CircleHotSpot>
    {
        [Parameter]
        public int Radius { get; set; }

        [Parameter]
        public int X { get; set; }

        [Parameter]
        public int Y { get; set; }
    }
}
