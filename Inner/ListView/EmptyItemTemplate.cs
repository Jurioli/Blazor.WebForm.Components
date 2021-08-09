using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class EmptyItemTemplate : TemplatePropertyComponent<System.Web.UI.WebControls.ListView>
    {
        protected override string PropertyName
        {
            get
            {
                return nameof(EmptyItemTemplate);
            }
        }
    }
}
