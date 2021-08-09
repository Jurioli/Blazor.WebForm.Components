using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class ListViewWebControlComponent<TControl> : ControlComponentBase<TControl>
        where TControl : WebControl, new()
    {
        [Parameter]
        public string Style
        {
            get
            {
                return this.Control.Style.Value;
            }
            set
            {
                this.Control.Style.Value = value;
            }
        }

        [Parameter]
        public bool Enabled
        {
            get
            {
                return this.Control.Enabled;
            }
            set
            {
                this.Control.Enabled = value;
            }
        }
    }
}
