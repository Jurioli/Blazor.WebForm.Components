using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class WebControlPropertyComponent<T, TControl> : ControlPropertyComponent<T, TControl>
        where TControl : System.Web.UI.WebControls.WebControl, new()
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
        public System.Web.UI.WebControls.BorderStyle BorderStyle
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
                this.Control.BorderWidth = this.ConvertFromString<System.Web.UI.WebControls.Unit>(value);
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
                this.Control.Height = this.ConvertFromString<System.Web.UI.WebControls.Unit>(value);
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
                this.Control.Width = this.ConvertFromString<System.Web.UI.WebControls.Unit>(value);
            }
        }

        [Parameter]
        public bool FontBold
        {
            get
            {
                return this.Control.Font.Bold;
            }
            set
            {
                this.Control.Font.Bold = value;
            }
        }

        [Parameter]
        public bool FontItalic
        {
            get
            {
                return this.Control.Font.Italic;
            }
            set
            {
                this.Control.Font.Italic = value;
            }
        }

        [Parameter]
        public string FontNames
        {
            get
            {
                return this.ConvertToString<System.Web.UI.WebControls.FontNamesConverter, string[]>(this.Control.Font.Names);
            }
            set
            {
                this.Control.Font.Names = this.ConvertFromString<System.Web.UI.WebControls.FontNamesConverter, string[]>(value);
            }
        }

        [Parameter]
        public bool FontOverline
        {
            get
            {
                return this.Control.Font.Overline;
            }
            set
            {
                this.Control.Font.Overline = value;
            }
        }

        [Parameter]
        public string FontSize
        {
            get
            {
                return this.ConvertToString(this.Control.Font.Size);
            }
            set
            {
                this.Control.Font.Size = this.ConvertFromString<System.Web.UI.WebControls.FontUnit>(value);
            }
        }

        [Parameter]
        public bool FontStrikeout
        {
            get
            {
                return this.Control.Font.Strikeout;
            }
            set
            {
                this.Control.Font.Strikeout = value;
            }
        }

        [Parameter]
        public bool FontUnderline
        {
            get
            {
                return this.Control.Font.Underline;
            }
            set
            {
                this.Control.Font.Underline = value;
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
