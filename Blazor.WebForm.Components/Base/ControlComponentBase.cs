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
        private bool _inSetParams;

        private bool _callbacking = false;

        private IReadOnlyDictionary<string, object> _parameters;
        private bool _renderedWithCascading;
        private bool _renderedWithInner;
        private bool _captureReference;
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

        [CascadingParameter(Name = nameof(System.Web.UI.Control.ContainerParent))]
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
                    _events = new EventHandlerDictionary(() => _inSetParams);
                }
                return _events;
            }
        }

        ICollection<string> IControlParameterViewComponent.ReserveParameters { get; set; }

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

        protected void InvokePropertyBindEvent<TEventArgs, TValue>(string propertyName, object sender, TEventArgs e, string valuePropertyName, TValue value)
        {
            if (_attributes != null
                && _attributes.TryGetValue($"{valuePropertyName}Changed", out object attribute)
                && attribute is EventCallback<TValue> callback
                && callback.HasDelegate)
            {
                try
                {
                    _callbacking = true;
                    if (_events != null && _events.TryGetEventCallbackAdapter(propertyName, callback, out EventCallbackAdapter<TEventArgs, TValue> callbackAdapter))
                    {
                        callbackAdapter.InvokeAsync(value, sender, e);
                    }
                    else
                    {
                        callback.InvokeAsync(value);
                    }
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

        protected override Task OnSetParametersAsync(ParameterViewContext context)
        {
            if (_firstSet)
            {
                bool filter = false;
                if (context.TryGetValue(nameof(this._ref), out Expression<Func<TControl>> func) && func != null)
                {
                    TControl control = this.CaptureReferenceControl(func);
                    if (control != null)
                    {
                        this.Control = control;
                        _captureReference = true;
                        if (control.IsPostBack || (control as IHandleSetInner).Rendered)
                        {
                            filter = true;
                            _renderedWithCascading = true;
                            _renderedWithInner = true;
                        }
                    }
                }
                _firstSet = false;
                _parameters = filter ? this.FilterParameters(context) : context.Parameters;
            }
            else
            {
                _parameters = this.FilterParameters(context);
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

        protected override void OnAfterRender(bool firstRender)
        {
            _parameters = null;
            base.OnAfterRender(firstRender);
        }

        protected override RenderFragment Render<TValue>(TValue control)
        {
            if (_captureReference)
            {
                (control as IHandleSetInner).Rendered = true;
            }
            return base.Render(control);
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
                this.SendMessage(nameof(this.RequestRefresh), this.TemplateControl);
            }
        }

        [MessageNotifyMethod]
        protected internal void RequestRefresh(TemplateControl control)
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
                this.SendMessage(nameof(this.RequestLoadPostData), this.TemplateControl);
            }
            else
            {
                this.SendMessage(nameof(this.RequestLoadPostDataAsync), this.TemplateControl);
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
