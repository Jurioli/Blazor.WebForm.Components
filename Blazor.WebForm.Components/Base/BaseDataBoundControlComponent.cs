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
    public abstract class BaseDataBoundControlComponent<TControl> : WebControlComponent<TControl>
        where TControl : BaseDataBoundControl, new()
    {
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

        //[Parameter]
        //public object DataSource
        //{
        //    get
        //    {
        //        return this.Control.DataSource;
        //    }
        //    set
        //    {
        //        this.Control.DataSource = value;
        //    }
        //}

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