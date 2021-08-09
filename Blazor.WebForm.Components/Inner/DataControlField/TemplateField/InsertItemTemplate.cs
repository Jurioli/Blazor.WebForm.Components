using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.TemplateField), typeof(InsertItemTemplate<>.TemplateFieldInsertItemTemplate))]
    public partial class InsertItemTemplate<TItem>
    {
        protected class TemplateFieldInsertItemTemplate : BindableTemplatePropertyComponentAdapter<InsertItemTemplate<TItem>, System.Web.UI.WebControls.TemplateField>
        {

        }
    }
}
