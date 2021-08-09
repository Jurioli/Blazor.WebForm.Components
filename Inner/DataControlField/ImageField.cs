using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class ImageField : DataControlFieldPropertyComponent<System.Web.UI.WebControls.ImageField>
    {
        [Parameter]
        public string NullDisplayText { get; set; }

        [Parameter]
        public string DataImageUrlFormatString { get; set; }

        [Parameter]
        public string DataImageUrlField { get; set; }

        [Parameter]
        public string DataAlternateTextFormatString { get; set; }

        [Parameter]
        public string DataAlternateTextField { get; set; }

        [Parameter]
        public bool ConvertEmptyStringToNull { get; set; }

        [Parameter]
        public string AlternateText { get; set; }

        [Parameter]
        public string NullImageUrl { get; set; }

        [Parameter]
        public bool ReadOnly { get; set; }
    }
}
