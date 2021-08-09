using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class ButtonField : ButtonFieldBasePropertyComponent<System.Web.UI.WebControls.ButtonField>
    {
        [Parameter]
        public string CommandName { get; set; }

        [Parameter]
        public string DataTextField { get; set; }

        [Parameter]
        public string DataTextFormatString { get; set; }

        [Parameter]
        public string ImageUrl { get; set; }

        [Parameter]
        public string Text { get; set; }
    }
}
