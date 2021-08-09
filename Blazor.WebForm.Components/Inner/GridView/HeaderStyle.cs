﻿using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.GridView), typeof(GridViewHeaderStyle))]
    public partial class HeaderStyle
    {
        protected class GridViewHeaderStyle : PropertyComponentAdapter<HeaderStyle, System.Web.UI.WebControls.GridView>
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.HeaderStyle.Apply(() => parameters);
                this.Owner.HeaderStyle.Font.Apply(() => this.Component.Font);
            }
        }
    }
}
