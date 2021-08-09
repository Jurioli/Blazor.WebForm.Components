using Extensions.Web.UI;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class UserControlComponent<TControl> : ControlComponentBase<TControl>
        where TControl : UserControl, new()
    {
        [Parameter]
        public string SortExpression
        {
            get
            {
                return this.Control.SortExpression;
            }
            set
            {
                this.Control.SortExpression = value;
            }
        }

        [Parameter]
        public string DataMember
        {
            get
            {
                return this.Control.DataMember;
            }
            set
            {
                this.Control.DataMember = value;
            }
        }

        [Parameter]
        public string DataSourceID
        {
            get
            {
                return this.Control.DataSourceID;
            }
            set
            {
                this.Control.DataSourceID = value;
            }
        }

        [Parameter]
        public bool ReadOnly
        {
            get
            {
                return this.Control.ReadOnly;
            }
            set
            {
                this.Control.ReadOnly = value;
            }
        }

        [Parameter]
        public EventHandler OnDataBound
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.DataBound += i, i => this.Control.DataBound -= i);
            }
        }
    }
}
