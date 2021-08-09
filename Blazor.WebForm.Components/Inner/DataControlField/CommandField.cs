using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class CommandField : ButtonFieldBasePropertyComponent<System.Web.UI.WebControls.CommandField>
    {
        [Parameter]
        public bool ShowInsertButton { get; set; }

        [Parameter]
        public bool ShowSelectButton { get; set; }

        [Parameter]
        public bool ShowEditButton { get; set; }

        [Parameter]
        public bool ShowDeleteButton { get; set; }

        [Parameter]
        public bool ShowCancelButton { get; set; }

        [Parameter]
        public string SelectText { get; set; }

        [Parameter]
        public string SelectImageUrl { get; set; }

        [Parameter]
        public string NewText { get; set; }

        [Parameter]
        public string NewImageUrl { get; set; }

        [Parameter]
        public string InsertText { get; set; }

        [Parameter]
        public string UpdateImageUrl { get; set; }

        [Parameter]
        public string InsertImageUrl { get; set; }

        [Parameter]
        public string EditImageUrl { get; set; }

        [Parameter]
        public string DeleteText { get; set; }

        [Parameter]
        public string DeleteImageUrl { get; set; }

        [Parameter]
        public string CancelText { get; set; }

        [Parameter]
        public string CancelImageUrl { get; set; }

        [Parameter]
        public string EditText { get; set; }

        [Parameter]
        public string UpdateText { get; set; }
    }
}
