using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class TableCellControlPropertyComponent<T, TControl> : WebControlPropertyComponent<T, TControl>
        where TControl : System.Web.UI.WebControls.TableCell, new()
    {
        [Parameter]
        public string AssociatedHeaderCellID
        {
            get
            {
                return this.ConvertToString<System.Web.UI.WebControls.StringArrayConverter, string[]>(this.Control.AssociatedHeaderCellID);
            }
            set
            {
                this.Control.AssociatedHeaderCellID = this.ConvertFromString<System.Web.UI.WebControls.StringArrayConverter, string[]>(value);
            }
        }

        [Parameter]
        public int ColumnSpan
        {
            get
            {
                return this.Control.ColumnSpan;
            }
            set
            {
                this.Control.ColumnSpan = value;
            }
        }

        [Parameter]
        public System.Web.UI.WebControls.HorizontalAlign HorizontalAlign
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
        public int RowSpan
        {
            get
            {
                return this.Control.RowSpan;
            }
            set
            {
                this.Control.RowSpan = value;
            }
        }

        [Parameter]
        public string Text
        {
            get
            {
                return this.Control.Text;
            }
            set
            {
                this.Control.Text = value;
            }
        }

        [Parameter]
        public System.Web.UI.WebControls.VerticalAlign VerticalAlign
        {
            get
            {
                return this.Control.VerticalAlign;
            }
            set
            {
                this.Control.VerticalAlign = value;
            }
        }

        [Parameter]
        public bool Wrap
        {
            get
            {
                return this.Control.Wrap;
            }
            set
            {
                this.Control.Wrap = value;
            }
        }
    }
}
