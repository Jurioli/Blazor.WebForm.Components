using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.FormView), typeof(EditItemTemplate<>.FormViewEditItemTemplate))]
    public partial class EditItemTemplate<TItem>
    {
        protected class FormViewEditItemTemplate : BindableTemplatePropertyComponentAdapter<EditItemTemplate<TItem>, System.Web.UI.WebControls.FormView>
        {

        }
    }
}
