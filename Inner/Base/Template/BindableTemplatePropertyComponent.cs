using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class BindableTemplatePropertyComponent<T, TItem> : PropertyComponent<T>
        where T : class
    {
        [Parameter]
        public RenderFragment<TItem> ChildContent { get; set; }

        protected abstract string PropertyName { get; }

        protected override bool NeedConvertParameter
        {
            get
            {
                return true;
            }
        }

        protected override KeyValuePair<string, object>? OnConvertParameter(KeyValuePair<string, object> parameter)
        {
            if (parameter.Key == nameof(this.ChildContent))
            {
                return new KeyValuePair<string, object>(this.PropertyName, this.GetTemplateProperty(this.ChildContent));
            }
            return base.OnConvertParameter(parameter);
        }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Owner.Apply(() => parameters);
        }

        protected abstract class BindableTemplatePropertyComponentAdapter<TComponent, TOwner> : PropertyComponentAdapter<TComponent, TOwner>
            where TComponent : BindableTemplatePropertyComponent<T, TItem>
            where TOwner : class, T
        {
            protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
            {
                this.Owner.Apply(() => parameters);
            }
        }
    }
}