using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class TemplateField : DataControlFieldPropertyComponent<System.Web.UI.WebControls.TemplateField>
    {
        [Parameter]
        public System.Web.UI.ValidateRequestMode ValidateRequestMode { get; set; }

        [Parameter]
        public bool ConvertEmptyStringToNull { get; set; }
    }
}
