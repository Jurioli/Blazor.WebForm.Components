using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class TableControlComponent<TControl> : WebControlComponent<TControl>
        where TControl : Table, new()
    {
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
        public string BackImageUrl
        {
            get
            {
                return this.Control.BackImageUrl;
            }
            set
            {
                this.Control.BackImageUrl = value;
            }
        }
    }
}
