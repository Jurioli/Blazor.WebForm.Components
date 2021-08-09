using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class RectangleHotSpot : HotSpotPropertyComponent<System.Web.UI.WebControls.RectangleHotSpot>
    {
        [Parameter]
        public int Bottom { get; set; }

        [Parameter]
        public int Left { get; set; }

        [Parameter]
        public int Right { get; set; }

        [Parameter]
        public int Top { get; set; }
    }
}
