using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class ButtonFieldBasePropertyComponent<TField> : DataControlFieldPropertyComponent<TField>
        where TField : System.Web.UI.WebControls.ButtonFieldBase, new()
    {
        [Parameter]
        public System.Web.UI.WebControls.ButtonType ButtonType { get; set; }

        [Parameter]
        public bool CausesValidation { get; set; }

        [Parameter]
        public string ValidationGroup { get; set; }
    }
}
