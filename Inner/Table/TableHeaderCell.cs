using Blazor.WebForm.UI;
using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class TableHeaderCell : TableCellControlPropertyComponent<System.Web.UI.WebControls.TableCellCollection, System.Web.UI.WebControls.TableHeaderCell>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string AbbreviatedText
        {
            get
            {
                return this.Control.AbbreviatedText;
            }
            set
            {
                this.Control.AbbreviatedText = value;
            }
        }

        [Parameter]
        public System.Web.UI.WebControls.TableHeaderScope Scope
        {
            get
            {
                return this.Control.Scope;
            }
            set
            {
                this.Control.Scope = value;
            }
        }

        [Parameter]
        public string CategoryText
        {
            get
            {
                return this.ConvertToString<System.Web.UI.WebControls.StringArrayConverter, string[]>(this.Control.CategoryText);
            }
            set
            {
                this.Control.CategoryText = this.ConvertFromString<System.Web.UI.WebControls.StringArrayConverter, string[]>(value);
            }
        }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(nameof(this.ChildContent)) && this.ChildContent != null)
            {
                System.Web.UI.RenderFragmentControl contentControl = new System.Web.UI.RenderFragmentControl();
                contentControl.Content = this.ChildContent;
                contentControl.Container = this.Control;
                this.Control.Controls.Add(contentControl);
            }
            this.Owner.Add(this.Control);
        }
    }
}
