using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class StylePropertyComponent<T> : PropertyComponent<T>
    {
        private Dictionary<string, object> _font;

        [Parameter]
        public string Height { get; set; }

        [Parameter]
        public string ForeColor { get; set; }

        [Parameter]
        public bool FontBold { get; set; }

        [Parameter]
        public bool FontItalic { get; set; }

        [Parameter]
        public string FontNames { get; set; }

        [Parameter]
        public bool FontOverline { get; set; }

        [Parameter]
        public string FontSize { get; set; }

        [Parameter]
        public bool FontStrikeout { get; set; }

        [Parameter]
        public bool FontUnderline { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public BorderStyle BorderStyle { get; set; }

        [Parameter]
        public string BorderWidth { get; set; }

        [Parameter]
        public string BorderColor { get; set; }

        [Parameter]
        public string BackColor { get; set; }

        [Parameter]
        public string Width { get; set; }

        protected override bool NeedConvertParameter
        {
            get
            {
                return true;
            }
        }

        protected IReadOnlyDictionary<string, object> Font
        {
            get
            {
                return _font;
            }
        }

        private Dictionary<string, object> FontInternal
        {
            get
            {
                if (_font == null)
                {
                    _font = new Dictionary<string, object>();
                }
                return _font;
            }
        }

        protected override KeyValuePair<string, object>? OnConvertParameter(KeyValuePair<string, object> parameter)
        {
            switch (parameter.Key)
            {
                case nameof(this.ForeColor):
                case nameof(this.BorderColor):
                case nameof(this.BackColor):
                    return this.ConvertFromString<Color>(parameter);

                case nameof(this.Height):
                case nameof(this.Width):
                case nameof(this.BorderWidth):
                    return this.ConvertFromString<Unit>(parameter);

                case nameof(this.FontBold):
                    this.FontInternal[nameof(FontInfo.Bold)] = parameter.Value;
                    return null;
                case nameof(this.FontItalic):
                    this.FontInternal[nameof(FontInfo.Italic)] = parameter.Value;
                    return null;
                case nameof(this.FontNames):
                    this.FontInternal[nameof(FontInfo.Names)] = this.ConvertFromString<FontNamesConverter, string[]>(parameter).Value;
                    return null;
                case nameof(this.FontOverline):
                    this.FontInternal[nameof(FontInfo.Overline)] = parameter.Value;
                    return null;
                case nameof(this.FontSize):
                    this.FontInternal[nameof(FontInfo.Size)] = this.ConvertFromString<FontUnit>(parameter).Value;
                    return null;
                case nameof(this.FontStrikeout):
                    this.FontInternal[nameof(FontInfo.Strikeout)] = parameter.Value;
                    return null;
                case nameof(this.FontUnderline):
                    this.FontInternal[nameof(FontInfo.Underline)] = parameter.Value;
                    return null;
            }
            return base.OnConvertParameter(parameter);
        }

        protected internal override void OnAfterRender()
        {
            if (_font != null)
            {
                _font = null;
            }
        }
    }
}
