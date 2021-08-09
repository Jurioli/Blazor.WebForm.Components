using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public partial class SelectedItemTemplate<TItem> : BindableTemplatePropertyComponent<object, TItem>, ICommonPropertyComponent
    {
        protected override string PropertyName
        {
            get
            {
                return nameof(SelectedItemTemplate<TItem>);
            }
        }
    }
}
