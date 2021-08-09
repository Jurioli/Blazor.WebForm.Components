using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class CheckBoxField : BoundFieldPropertyComponent<System.Web.UI.WebControls.CheckBoxField>
    {
        [Parameter]
        public string Text { get; set; }
    }
}
