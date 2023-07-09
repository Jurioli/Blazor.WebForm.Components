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
    public abstract class ListControlComponent<TControl> : DataBoundControlComponent<TControl>
        where TControl : ListControl, new()
    {
        internal const string OnDataBindingSelectedIndexChanged = "OnDataBindingSelectedIndexChanged";

        private bool _hasBindSelectedValue;

        [Parameter]
        public string ValidationGroup
        {
            get
            {
                return this.Control.ValidationGroup;
            }
            set
            {
                this.Control.ValidationGroup = value;
            }
        }

        [Parameter]
        public virtual string SelectedValue
        {
            get
            {
                return this.Control.SelectedValue;
            }
            set
            {
                if (_hasBindSelectedValue)
                {
                    ((IBindingListControl)this.Control).SelectedValue = value;
                }
                else
                {
                    this.Control.SelectedValue = value;
                }
            }
        }

        [Parameter]
        public string DataValueField
        {
            get
            {
                return this.Control.DataValueField;
            }
            set
            {
                this.Control.DataValueField = value;
            }
        }

        [Parameter]
        public string DataTextFormatString
        {
            get
            {
                return this.Control.DataTextFormatString;
            }
            set
            {
                this.Control.DataTextFormatString = value;
            }
        }

        [Parameter]
        public string DataTextField
        {
            get
            {
                return this.Control.DataTextField;
            }
            set
            {
                this.Control.DataTextField = value;
            }
        }

        [Parameter]
        public bool CausesValidation
        {
            get
            {
                return this.Control.CausesValidation;
            }
            set
            {
                this.Control.CausesValidation = value;
            }
        }

        [Parameter]
        public bool AppendDataBoundItems
        {
            get
            {
                return this.Control.AppendDataBoundItems;
            }
            set
            {
                this.Control.AppendDataBoundItems = value;
            }
        }

        [Parameter]
        public bool AutoPostBack
        {
            get
            {
                return this.Control.AutoPostBack;
            }
            set
            {
                this.Control.AutoPostBack = value;
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

        [Parameter]
        public EventHandler OnTextChanged
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.TextChanged += i, i => this.Control.TextChanged -= i);
            }
        }

        //public string Text
        //{
        //    get
        //    {
        //        return this.Control.Text;
        //    }
        //    set
        //    {
        //        this.Control.Text = value;
        //    }
        //}

        //public System.Web.UI.WebControls.ListItem SelectedItem
        //{
        //    get
        //    {
        //        return this.Control.SelectedItem;
        //    }
        //}

        //public int SelectedIndex
        //{
        //    get
        //    {
        //        return this.Control.SelectedIndex;
        //    }
        //    set
        //    {
        //        this.Control.SelectedIndex = value;
        //    }
        //}

        //public ListItemCollection Items
        //{
        //    get
        //    {
        //        return this.Control.Items;
        //    }
        //}

        //public void ClearSelection()
        //{
        //    this.Control.ClearSelection();
        //}

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (this.HasPropertyBindEvent<string>(nameof(this.SelectedValue)))
            {
                _hasBindSelectedValue = true;
                this.Control.AutoPostBack = true;
                this.SetBindEventProperty(nameof(this.OnSelectedIndexChanged), this.BindSelectedIndexChanged, i => this.Control.SelectedIndexChanged += i, i => this.Control.SelectedIndexChanged -= i);
                this.SetBindEventProperty(OnDataBindingSelectedIndexChanged, this.BindDataBindingSelectedIndexChanged, i => ((IBindingListControl)this.Control).DataBindingSelectedIndexChanged += i, i => ((IBindingListControl)this.Control).DataBindingSelectedIndexChanged -= i);
            }
        }

        private void BindSelectedIndexChanged(object sender, EventArgs e)
        {
            this.InvokePropertyBindEvent(nameof(this.OnSelectedIndexChanged), sender, e, nameof(this.SelectedValue), this.SelectedValue);
        }

        private void BindDataBindingSelectedIndexChanged(object sender, EventArgs e)
        {
            this.InvokePropertyBindEvent(OnDataBindingSelectedIndexChanged, sender, e, nameof(this.SelectedValue), this.SelectedValue);
        }

        protected override void SetInnerPropertyWithCascading(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters.TryGetValue(nameof(this.SelectedValue), out object value) && value is string selectedValue)
            {
                if (this.SelectedValue != selectedValue)
                {
                    this.SelectedValue = selectedValue;
                }
            }
        }
    }
}
