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
        where TControl : ListControl, new()
    {
        [Parameter]
        public override string SelectedValue
        {
            get
            {
                return base.SelectedValue;
            }
            set
            {
                if (this.IsMultipleSelection && !string.IsNullOrEmpty(value))
                {
                    foreach (ListItem item in this.Control.Items)
                    {
                        if (item.Selected)
                        {
                            if (item.Value == value)
                            {
                                return;
                            }
                            break;
                        }
                    }
                }
                base.SelectedValue = value;
            }
        }

        [Parameter]
        public string[] SelectedValues
        {
            get
            {
                List<string> list = new List<string>();
                foreach (ListItem item in this.Control.Items)
                {
                    if (item.Selected)
                    {
                        list.Add(item.Value);
                    }
                }
                return list.ToArray();
            }
            set
            {
                bool autoPostBack = this.AutoPostBack;
                if (autoPostBack)
                {
                    this.AutoPostBack = false;
                }
                try
                {
                    foreach (ListItem item in this.Control.Items)
                    {
                        item.Selected = (Array.IndexOf(value, item.Value) != -1);
                    }
                }
                finally
                {
                    this.AutoPostBack = autoPostBack;
                }
            }
        }

        protected abstract bool IsMultipleSelection { get; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (this.HasPropertyBindEvent<string[]>(nameof(this.SelectedValues)))
            {
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
