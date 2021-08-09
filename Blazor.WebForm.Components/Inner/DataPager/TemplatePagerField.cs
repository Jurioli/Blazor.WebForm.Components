using Blazor.WebForm.UI;
using Blazor.WebForm.UI.PropertyComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp
{
    public class TemplatePagerField : DataPagerFieldPropertyComponent
    {
        [Parameter]
        public RenderFragment PagerTemplate { get; set; }

        [Parameter]
        public EventHandler<System.Web.UI.WebControls.DataPagerCommandEventArgs> OnPagerCommand { get; set; }

        protected internal override void SetInnerProperty(IReadOnlyDictionary<string, object> parameters)
        {
            System.Web.UI.WebControls.TemplatePagerField field = new System.Web.UI.WebControls.TemplatePagerField();
            if (parameters.ContainsKey(nameof(this.PagerTemplate)))
            {
                field.PagerTemplate = this.GetTemplateProperty(this.PagerTemplate);
            }
            if (parameters.ContainsKey(nameof(this.OnPagerCommand)))
            {
                this.SetEventProperty(this.OnPagerCommand, i => field.PagerCommand += i, i => field.PagerCommand -= i, nameof(this.OnPagerCommand));
            }
            this.Owner.Fields.Add(field);
        }
    }
}
