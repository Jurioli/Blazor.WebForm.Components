using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class ControlPropertyComponent<T, TControl> : PropertyComponent<T>, IControlParameterViewComponent
        where TControl : System.Web.UI.Control, new()
    {
        private IReadOnlyDictionary<string, object> _attributes;
        private bool _firstSet = true;

        private TControl _control;

        protected TControl Control
        {
            get
            {
                if (_control == null)
                {
                    _control = new TControl();
                }
                return _control;
            }
        }

        //new public virtual TControl Control
        //{
        //    get
        //    {
        //        return base.Control;
        //    }
        //}

        //new public virtual ControlCollection Controls
        //{
        //    get
        //    {
        //        return this.Control.Controls;
        //    }
        //}

        [Parameter]
        public Expression<Func<TControl>> _ref { get; set; }

        [Parameter]
        public string ID
        {
            get
            {
                return this.Control.ID;
            }
            set
            {
                this.Control.ID = value;
            }
        }

        [Parameter]
        public System.Web.UI.ClientIDMode ClientIDMode
        {
            get
            {
                return this.Control.ClientIDMode;
            }
            set
            {
                this.Control.ClientIDMode = value;
            }
        }

        [Parameter]
        public bool Visible
        {
            get
            {
                return this.Control.Visible;
            }
            set
            {
                this.Control.Visible = value;
            }
        }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> Attributes
        {
            get
            {
                return _attributes;
            }
            set
            {
                this.SetAttributes(value);
            }
        }

        [Parameter]
        public EventHandler OnPreRender
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.PreRender += i, i => this.Control.PreRender -= i);
            }
        }

        [Parameter]
        public EventHandler OnUnload
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.Unload += i, i => this.Control.Unload -= i);
            }
        }

        [Parameter]
        public EventHandler OnLoad
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.Load += i, i => this.Control.Load -= i);
            }
        }

        [Parameter]
        public EventHandler OnInit
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.Init += i, i => this.Control.Init -= i);
            }
        }

        [Parameter]
        public EventHandler OnDataBinding
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.DataBinding += i, i => this.Control.DataBinding -= i);
            }
        }

        [Parameter]
        public EventHandler OnDisposed
        {
            get
            {
                return this.GetEventProperty();
            }
            set
            {
                this.SetEventProperty(value, i => this.Control.Disposed += i, i => this.Control.Disposed -= i);
            }
        }

        System.Web.UI.Control IControlParameterViewComponent.Control
        {
            get
            {
                return this.Control;
            }
        }

        IReadOnlyDictionary<string, object> IControlParameterViewComponent.Attributes
        {
            get
            {
                return _attributes;
            }
            set
            {
                _attributes = value;
            }
        }

        ICollection<string> IControlParameterViewComponent.ReserveParameters { get; set; }

        protected override Task OnSetParametersAsync(ParameterViewContext context)
        {
            if (_firstSet)
            {
                if (context.TryGetValue(nameof(this._ref), out Expression<Func<TControl>> func) && func != null)
                {
                    TControl control = this.CaptureReferenceControl(func);
                    if (control != null)
                    {
                        this._control = control;
                    }
                }
                _firstSet = false;
            }
            else
            {
                this.FilterParameters(context);
            }
            try
            {
                _inSetParams = true;
                return base.OnSetParametersAsync(context);
            }
            finally
            {
                _inSetParams = false;
            }
        }

        //public void DataBind()
        //{
        //    this.Control.DataBind();
        //}

        //public void Focus()
        //{
        //    this.Control.Focus();
        //}

        public static implicit operator TControl(ControlPropertyComponent<T, TControl> component)
        {
            return component._control;
        }
    }
}
