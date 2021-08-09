using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class HotSpotPropertyComponent<TSpot> : PropertyComponent<System.Web.UI.WebControls.ImageMap>
        where TSpot : System.Web.UI.WebControls.HotSpot, new()
    {
        [Parameter]
        public string AccessKey { get; set; }

        [Parameter]
        public string AlternateText { get; set; }

        [Parameter]
        public System.Web.UI.WebControls.HotSpotMode HotSpotMode { get; set; }

        [Parameter]
        public string PostBackValue { get; set; }

        [Parameter]
        public string NavigateUrl { get; set; }

        [Parameter]
        public short TabIndex { get; set; }

        [Parameter]
        public string Target { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.HotSpots.Add(new TSpot().Apply(() => parameters));
        }
    }
}
