﻿using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.GridView), typeof(GridViewPagerStyle))]
    public partial class PagerStyle
    {
        protected class GridViewPagerStyle : PropertyComponentAdapter<PagerStyle, System.Web.UI.WebControls.GridView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.PagerStyle.Apply(() => parameters);
                this.Owner.PagerStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
