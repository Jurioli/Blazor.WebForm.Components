using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class ViewControlPropertyComponent<T, TControl> : ControlPropertyComponent<T, TControl>
        where TControl : System.Web.UI.WebControls.View, new()
    {
        [Parameter]
        public EventHandler OnActivate
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.Activate += i, i => this.Control.Activate -= i);
            }
        }

        [Parameter]
        public EventHandler OnDeactivate
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.Deactivate += i, i => this.Control.Deactivate -= i);
            }
        }
    }
}
