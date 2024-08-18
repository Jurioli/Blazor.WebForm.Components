using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class MultipleSelectionListControlComponent<TControl> : ListControlComponent<TControl>
        where TControl : MultipleSelectionListControl, new()
    {
        private bool _hasBindSelectedValues;

        [Parameter]
        public string[] SelectedValues
        {
            get
            {
                return this.Control.SelectedValues;
            }
            set
            {
                if (_hasBindSelectedValues)
                {
                    ((IBindingMultipleSelectionListControl)this.Control).SelectedValues = value;
                }
                else
                {
                    this.Control.SelectedValues = value;
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (this.HasPropertyBindEvent<string[]>(nameof(this.SelectedValues)))
            {
                _hasBindSelectedValues = true;
                this.Control.AutoPostBack = true;
                this.SetBindEventProperty(nameof(this.OnSelectedIndexChanged), this.BindSelectedIndexChanged, i => this.Control.SelectedIndexChanged += i, i => this.Control.SelectedIndexChanged -= i);
                this.SetBindEventProperty(OnDataBindingSelectedIndexChanged, this.BindSelectedIndexChanged, i => ((IBindingListControl)this.Control).DataBindingSelectedIndexChanged += i, i => ((IBindingListControl)this.Control).DataBindingSelectedIndexChanged -= i);
            }
        }

        private void BindSelectedIndexChanged(object sender, EventArgs e)
        {
            this.InvokePropertyBindEvent(nameof(this.OnSelectedIndexChanged), sender, e, nameof(this.SelectedValues), this.SelectedValues);
        }

        private void BindDataBindingSelectedIndexChanged(object sender, EventArgs e)
        {
            this.InvokePropertyBindEvent(OnDataBindingSelectedIndexChanged, sender, e, nameof(this.SelectedValues), this.SelectedValues);
        }

        protected override void SetInnerPropertyWithCascading(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters.TryGetValue(nameof(this.SelectedValues), out object value) && value is string[] selectedValues)
            {
                if (this.SelectedValues != selectedValues)
                {
                    this.SelectedValues = selectedValues;
                }
            }
            base.SetInnerPropertyWithCascading(parameters);
        }
    }
}
