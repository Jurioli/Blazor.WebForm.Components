using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    public class FinishNavigationTemplate : TemplatePropertyComponent<System.Web.UI.WebControls.Wizard>
    {
        protected override string PropertyName
        {
            get
            {
                return nameof(FinishNavigationTemplate);
            }
        }
    }
}
