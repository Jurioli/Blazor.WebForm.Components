using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeConverter = System.ComponentModel.TypeConverter;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class PropertyComponent<T> : IParameterViewComponent
    {
        private RenderHandle _renderHandle;
        private bool _initialized;
        private IReadOnlyDictionary<string, object> _parameters;
        private RenderFragment _renderFragment;

        private EventHandlerDictionary _events;

        [CascadingParameter]
        protected internal T Owner { get; set; }

        private PropertyComponentAdapter<T> AdapterInternal { get; set; }

        protected virtual bool NeedConvertParameter
        {
            get
            {
                return false;
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

        void IComponent.Attach(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
        }

        Task IComponent.SetParametersAsync(ParameterView parameters)
        {
            return this.SetParametersAsync(parameters);
        }

        public virtual Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            if (this.Owner != null && !_initialized)
            {
                this.AdapterInternal = this.ResolveAdapter();
                IReadOnlyDictionary<string, object> dictionary;
                if (!this.NeedConvertParameter)
                {
                    dictionary = parameters.ToDictionary();
                }
                else
                {
                    dictionary = new Dictionary<string, object>(this.GetConvertParameters(parameters));
                }
                _parameters = dictionary;
                this.SetInnerPropertyInternal(_parameters);
                if (_renderFragment != null)
                {
                    _renderHandle.Render(_renderFragment);
                }
                else
                {
                    _parameters = null;
                    this.OnAfterRenderInternal();
                }
                _initialized = true;
            }
            return Task.CompletedTask;
        }

        private IEnumerable<KeyValuePair<string, object>> GetConvertParameters(ParameterView parameters)
        {
            foreach (KeyValuePair<string, object> parameter in parameters.ToDictionary())
            {
                KeyValuePair<string, object>? pair = this.OnConvertParameter(parameter);
                if (pair != null)
                {
                    yield return pair.Value;
                }
            }
        }

        protected virtual KeyValuePair<string, object>? OnConvertParameter(KeyValuePair<string, object> parameter)
        {
            return parameter;
        }

        protected KeyValuePair<string, object> ConvertFromString<TValue>(KeyValuePair<string, object> parameter)
        {
            if (parameter.Value is string value)
            {
                return new KeyValuePair<string, object>(parameter.Key, this.ConvertFromString<TValue>(value));
            }
            else
            {
                return parameter;
            }
        }

        protected KeyValuePair<string, object> ConvertFromString<TConverter, TValue>(KeyValuePair<string, object> parameter)
            where TConverter : TypeConverter, new()
        {
            if (parameter.Value is string value)
            {
                return new KeyValuePair<string, object>(parameter.Key, this.ConvertFromString<TConverter, TValue>(value));
            }
            else
            {
                return parameter;
            }
        }

        private void SetInnerPropertyInternal(IReadOnlyDictionary<string, object> parameters)
        {
            if (this.AdapterInternal != null)
            {
                this.AdapterInternal.SetInnerProperty(parameters);
            }
            else
            {
                this.SetInnerProperty(parameters);
            }
        }

        private void SetInnerPropertyWithCascadingInternal(IReadOnlyDictionary<string, object> parameters)
        {
            if (this.AdapterInternal != null)
            {
                this.AdapterInternal.SetInnerPropertyWithCascading(parameters);
            }
            else
            {
                this.SetInnerPropertyWithCascading(parameters);
            }
        }

        private void OnAfterRenderInternal()
        {
            if (this.AdapterInternal != null)
            {
                this.AdapterInternal.OnAfterRender();
            }
            else
            {
                this.OnAfterRender();
            }
        }

        private PropertyComponentAdapter<T> ResolveAdapter()
        {
            if (this is ICommonPropertyComponent)
            {
                return CommonPropertyAdapterMapper.GetAdapter(this);
            }
            return null;
        }

        protected internal virtual void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {

        }

        protected internal virtual void SetInnerPropertyWithCascading(IReadOnlyDictionary<string, object> parameters)
        {

        }

        protected internal virtual void OnAfterRender()
        {

        }

        //private void Render()
        //{
        //    _parameters = null;
        //    _renderFragment = null;
        //    this.OnAfterRenderInternal();
        //}

        //protected void Render(RenderFragment childContent, int childLevel = 0)
        //{
        //    if (_initialized)
        //    {
        //        return;
        //    }
        //    _renderFragment = RenderUtility.Render(childContent, childLevel, this.Render);
        //}

        private void RenderWithCascading()
        {
            this.SetInnerPropertyWithCascadingInternal(_parameters);
            _parameters = null;
            _renderFragment = null;
            this.OnAfterRenderInternal();
        }

        protected void RenderWithCascading<TValue>(TValue property, RenderFragment childContent, int childLevel = 0)
        {
            if (_initialized)
            {
                return;
            }
            _renderFragment = RenderUtility.RenderWithCascading(property, childContent, childLevel, this.RenderWithCascading);
        }

        protected abstract class PropertyComponentAdapter<TComponent, TOwner> : PropertyComponentAdapter<T>
            where TComponent : PropertyComponent<T>
            where TOwner : T
        {
            new protected internal TComponent Component
            {
                get
                {
                    return (TComponent)base.Component;
                }
                internal set
                {
                    base.Component = value;
                }
            }
            protected TOwner Owner
            {
                get
                {
                    return (TOwner)this.Component.Owner;
                }
            }
        }
    }
}
