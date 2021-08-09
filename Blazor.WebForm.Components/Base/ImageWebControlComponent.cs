using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class ImageWebControlComponent<TControl> : ControlComponentBase<TControl>
        where TControl : WebControl, new()
    {
        [Parameter]
        public string ToolTip
        {
            get
            {
                return this.Control.ToolTip;
            }
            set
            {
                this.Control.ToolTip = value;
            }
        }

        [Parameter]
        public string CssClass
        {
            get
            {
                return this.Control.CssClass;
            }
            set
            {
                this.Control.CssClass = value;
            }
        }

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
        public BorderStyle BorderStyle
        {
            get
            {
                return this.Control.BorderStyle;
            }
            set
            {
                this.Control.BorderStyle = value;
            }
        }

        [Parameter]
        public string BorderWidth
        {
            get
            {
                return this.ConvertToString(this.Control.BorderWidth);
            }
            set
            {
                this.Control.BorderWidth = this.ConvertFromString<Unit>(value);
            }
        }

        [Parameter]
        public string Height
        {
            get
            {
                return this.ConvertToString(this.Control.Height);
            }
            set
            {
                this.Control.Height = this.ConvertFromString<Unit>(value);
            }
        }

        [Parameter]
        public string Width
        {
            get
            {
                return this.ConvertToString(this.Control.Width);
            }
            set
            {
                this.Control.Width = this.ConvertFromString<Unit>(value);
            }
        }

        [Parameter]
        public string BackColor
        {
            get
            {
                return this.ConvertToString(this.Control.BackColor);
            }
            set
            {
                this.Control.BackColor = this.ConvertFromString<Color>(value);
            }
        }

        [Parameter]
        public short TabIndex
        {
            get
            {
                return this.Control.TabIndex;
            }
            set
            {
                this.Control.TabIndex = value;
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

        [Parameter]
        public string AccessKey
        {
            get
            {
                return this.Control.AccessKey;
            }
            set
            {
                this.Control.AccessKey = value;
            }
        }

        [Parameter]
        public string BorderColor
        {
            get
            {
                return this.ConvertToString(this.Control.BorderColor);
            }
            set
            {
                this.Control.BorderColor = this.ConvertFromString<Color>(value);
            }
        }

        [Parameter]
        public string ForeColor
        {
            get
            {
                return this.ConvertToString(this.Control.ForeColor);
            }
            set
            {
                this.Control.ForeColor = this.ConvertFromString<Color>(value);
            }
        }
    }
}
