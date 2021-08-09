using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class HyperLinkField : DataControlFieldPropertyComponent<System.Web.UI.WebControls.HyperLinkField>
    {
        [Parameter]
        public string DataNavigateUrlFields { get; set; }

        [Parameter]
        public string DataNavigateUrlFormatString { get; set; }

        [Parameter]
        public string DataTextField { get; set; }

        [Parameter]
        public string DataTextFormatString { get; set; }

        [Parameter]
        public string NavigateUrl { get; set; }

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public string Text { get; set; }

        protected override bool NeedConvertParameter
        {
            get
            {
                return true;
            }
        }

        protected override KeyValuePair<string, object>? OnConvertParameter(KeyValuePair<string, object> parameter)
        {
            switch (parameter.Key)
            {
                case nameof(this.DataNavigateUrlFields):
                    return this.ConvertFromString<System.Web.UI.WebControls.StringArrayConverter, string[]>(parameter);
            }
            return base.OnConvertParameter(parameter);
        }
    }
}
