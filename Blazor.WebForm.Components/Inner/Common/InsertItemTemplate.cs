using Blazor.WebForm.UI;
using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public partial class InsertItemTemplate<TItem> : BindableTemplatePropertyComponent<object, TItem>, ICommonPropertyComponent
        where TItem : new()
    {
        protected override string PropertyName
        {
            get
            {
                return nameof(InsertItemTemplate<TItem>);
            }
        }

        protected override KeyValuePair<string, object>? OnConvertParameter(KeyValuePair<string, object> parameter)
        {
            if (parameter.Key == nameof(this.ChildContent))
            {
                return new KeyValuePair<string, object>(this.PropertyName, this.GetInsertTemplateProperty(this.ChildContent));
            }
            return base.OnConvertParameter(parameter);
        }
    }
}
