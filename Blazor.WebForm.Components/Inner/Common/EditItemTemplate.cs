using Blazor.WebForm.UI;
using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public partial class EditItemTemplate<TItem> : BindableTemplatePropertyComponent<object, TItem>, ICommonPropertyComponent
       where TItem : class, new()
    {
        [Parameter]
        public bool SubstituteItem { get; set; } = true;

        protected override string PropertyName
        {
            get
            {
                return nameof(EditItemTemplate<TItem>);
            }
        }

        protected override KeyValuePair<string, object>? OnConvertParameter(KeyValuePair<string, object> parameter)
        {
            if (parameter.Key == nameof(this.ChildContent) && this.SubstituteItem)
            {
                return new KeyValuePair<string, object>(this.PropertyName, this.GetEditTemplateProperty(this.ChildContent));
            }
            return base.OnConvertParameter(parameter);
        }
    }
}
