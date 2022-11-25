using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class ControlComponentBase<TControl> : ControlComponent<TControl>, IControlParameterViewComponent, IDisposable
        where TControl : Control, new()
    {
        private bool _initialized;
        private bool _disposed;

        private IReadOnlyDictionary<string, object> _attributes;
        private EventHandlerDictionary _events;
        private bool _firstSet = true;

        private bool _callbacking = false;

        private IReadOnlyDictionary<string, object> _parameters;
        private bool _renderedWithCascading;
        private bool _renderedWithInner;
        private TemplateControl _templateControl;

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
        public ClientIDMode ClientIDMode
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
        new public EventHandler OnLoad
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

        [CascadingParameter(Name = nameof(ContainerParent))]
        protected Control ContainerParent
        {
            get
            {
                return this.Control.ContainerParent;
            }
            set
            {
                this.Control.ContainerParent = value;
            }
        }

        protected bool Initialized
        {
            get
            {
                return _initialized;
            }
        }

        Control IControlParameterViewComponent.Control
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

        EventHandlerDictionary IParameterViewComponent.Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new EventHandlerDictionary();
                }
                return _events;
            }
        }

        List<string> IControlParameterViewComponent.ReserveParameters { get; set; }

        public TemplateControl TemplateControl
        {
            get
            {
                if (_templateControl == null)
                {
                    _templateControl = (this.Control as IVirtualNamingContainer).TemplateControl;
                }
                return _templateControl;
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

        protected override void OnInitialized()
        {
            if (!_initialized)
            {
                this.Page.Submit += this.OnSubmit;
                if (!string.IsNullOrEmpty(this.Control.ID))
                {
                    (this.Control as IVirtualNamingContainer).EnsureReferenceControl();
                }
                _initialized = true;
            }
        }

        protected bool HasPropertyBindEvent<TValue>(string propertyName)
        {
            if (_attributes != null
                && _attributes.TryGetValue($"{propertyName}Changed", out object attribute)
                && attribute is EventCallback<TValue> callback
                && callback.HasDelegate)
            {
                return true;
            }
            return false;
        }

        protected void InvokePropertyBindEvent<TValue>(string propertyName, TValue value)
        {
            if (_attributes != null
                && _attributes.TryGetValue($"{propertyName}Changed", out object attribute)
                && attribute is EventCallback<TValue> callback
                && callback.HasDelegate)
            {
                try
                {
                    _callbacking = true;
                    callback.InvokeAsync(value);
                }
                finally
                {
                    _callbacking = false;
                }
            }
        }

        protected override bool ShouldRender()
        {
            return !_callbacking && base.ShouldRender();
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            if (_firstSet)
            {
                if (parameters.TryGetValue(nameof(this._ref), out Expression<Func<TControl>> func) && func != null)
                {
                    TControl control = this.CaptureReferenceControl(func);
                    if (control != null)
                    {
                        this.Control = control;
                        if (control.IsPostBack)
                        {
                            this.FilterParameters(ref parameters);
                            _renderedWithCascading = true;
                            _renderedWithInner = true;
                        }
                    }
                }
                _firstSet = false;
            }
            else
            {
                this.FilterParameters(ref parameters);
            }
            _parameters = parameters.ToDictionary();
            return base.SetParametersAsync(parameters);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            _parameters = null;
            base.OnAfterRender(firstRender);
        }

        protected virtual void SetInnerPropertyWithCascading(IReadOnlyDictionary<string, object> parameters)
        {

        }

        private RenderFragment RenderWithCascading<TValue>(TValue control) where TValue : Control
        {
            this.SetInnerPropertyWithCascading(_parameters);
            return this.Render(control);
        }

        protected RenderFragment RenderWithCascading<TValue>(TValue control, RenderFragment childContent = null, int childLevel = 0) where TValue : Control
        {
            if (!_renderedWithCascading)
            {
                _renderedWithCascading = true;
                return RenderUtility.RenderWithCascading(control, childContent, childLevel, this.RenderWithCascading);
            }
            else
            {
                return this.Render(control);
            }
        }

        protected virtual void SetInnerPropertyWithInner(IReadOnlyDictionary<string, object> parameters)
        {

        }

        protected RenderFragment RenderWithInner<TValue>(TValue control) where TValue : Control
        {
            if (!_renderedWithInner)
            {
                _renderedWithInner = true;
                this.SetInnerPropertyWithInner(_parameters);
            }
            return this.Render(control);
        }

        protected override void OnUpdate(object sender, StateUpdateEventArgs e)
        {
            base.OnUpdate(sender, e);
            if (e.PostBackRefresh)
            {
                this.SendMessage("RequestRefresh", this.TemplateControl);
            }
        }

        [MessageNotifyMethod]
        protected void RequestRefresh(TemplateControl control)
        {
            if (_disposed || control != this.TemplateControl)
            {
                return;
            }
            this.StateHasChanged();
        }

        protected virtual void OnSubmit(object sender, EventArgs e)
        {
            if ((this.ClientScript as IPlatformClientScript).InProcess)
            {
                this.SendMessage("RequestLoadPostData", this.TemplateControl);
            }
            else
            {
                this.SendMessage("RequestLoadPostDataAsync", this.TemplateControl);
            }
        }

        [MessageNotifyMethod]
        protected void RequestLoadPostData(TemplateControl control)
        {
            if (_disposed || control != this.TemplateControl)
            {
                return;
            }
            if (this.Control is IPostBackDataHandler)
            {
                this.LoadPostData(false);
            }
        }

        [MessageNotifyMethod]
        protected async Task RequestLoadPostDataAsync(TemplateControl control)
        {
            if (_disposed || control != this.TemplateControl)
            {
                return;
            }
            if (this.Control is IPostBackDataHandler)
            {
                await this.LoadPostDataAsync(false);
            }
        }

        public static implicit operator TControl(ControlComponentBase<TControl> component)
        {
            return component.Control;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                (this as IHandleUnload).Unload(_events != null ? _events.RemoveAll : null);
                _disposed = true;
            }
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }
    }
}
