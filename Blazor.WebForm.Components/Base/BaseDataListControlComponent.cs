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
    public abstract class BaseDataListControlComponent<TControl> : WebControlComponent<TControl>
        where TControl : BaseDataList, new()
    {
        [Parameter]
        public string Caption
        {
            get
            {
                return this.Control.Caption;
            }
            set
            {
                this.Control.Caption = value;
            }
        }

        [Parameter]
        public int CellPadding
        {
            get
            {
                return this.Control.CellPadding;
            }
            set
            {
                this.Control.CellPadding = value;
            }
        }

        [Parameter]
        public int CellSpacing
        {
            get
            {
                return this.Control.CellSpacing;
            }
            set
            {
                this.Control.CellSpacing = value;
            }
        }

        [Parameter]
        public string DataKeyField
        {
            get
            {
                return this.Control.DataKeyField;
            }
            set
            {
                this.Control.DataKeyField = value;
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
        public bool UseAccessibleHeader
        {
            get
            {
                return this.Control.UseAccessibleHeader;
            }
            set
            {
                this.Control.UseAccessibleHeader = value;
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
        public GridLines GridLines
        {
            get
            {
                return this.Control.GridLines;
            }
            set
            {
                this.Control.GridLines = value;
            }
        }

        [Parameter]
        public HorizontalAlign HorizontalAlign
        {
            get
            {
                return this.Control.HorizontalAlign;
            }
            set
            {
                this.Control.HorizontalAlign = value;
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
        public TableCaptionAlign CaptionAlign
        {
            get
            {
                return this.Control.CaptionAlign;
            }
            set
            {
                this.Control.CaptionAlign = value;
            }
        }

        [Parameter]
        public EventHandler OnSelectedIndexChanged
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.SelectedIndexChanged += i, i => this.Control.SelectedIndexChanged -= i);
            }
        }
    }
}
