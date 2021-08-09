using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class PropertyComponentAdapter<T>
    {
        private PropertyComponent<T> _component;

        protected internal PropertyComponent<T> Component
        {
            get
            {
                return _component;
            }
            internal set
            {
                _component = value;
            }
        }

        protected internal virtual void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            this.Component.SetInnerProperty(parameters);
        }

        protected internal virtual void SetInnerPropertyWithCascading(IReadOnlyDictionary<string, object> parameters)
        {
            this.Component.SetInnerProperty(parameters);
        }

        protected internal virtual void OnAfterRender()
        {
            this.Component.OnAfterRender();
        }
    }
}
