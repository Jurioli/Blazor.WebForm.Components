using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class DataControlFieldPropertyComponent<TField> : PropertyComponent<System.Web.UI.WebControls.DataControlFieldCollection>
        where TField : System.Web.UI.WebControls.DataControlField, new()
    {
        private TField _field;

        private TField Field
        {
            get
            {
                if (_field == null)
                {
                    _field = new TField();
                }
                return _field;
            }
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string SortExpression { get; set; }

        [Parameter]
        public bool ShowHeader { get; set; }

        [Parameter]
        public bool InsertVisible { get; set; }

        [Parameter]
        public string HeaderText { get; set; }

        [Parameter]
        public string HeaderImageUrl { get; set; }

        [Parameter]
        public string FooterText { get; set; }

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public string AccessibleHeaderText { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Field.Apply(() => parameters);
            if (parameters.ContainsKey(nameof(this.ChildContent)) && this.ChildContent != null)
            {
                this.RenderWithCascading(this.Field, this.ChildContent);
            }
            else
            {
                this.Owner.Add(this.Field);
            }
        }

        protected internal override void SetInnerPropertyWithCascading(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.Add(this.Field);
        }
    }
}
