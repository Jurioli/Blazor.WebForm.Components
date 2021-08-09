using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public partial class EmptyDataTemplate : TemplatePropertyComponent<object>, ICommonPropertyComponent
    {
        protected override string PropertyName
        {
            get
            {
                return nameof(EmptyDataTemplate);
            }
        }
    }
}
