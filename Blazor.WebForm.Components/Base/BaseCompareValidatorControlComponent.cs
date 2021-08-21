using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI.ControlComponents
{
    public abstract class BaseCompareValidatorControlComponent<TControl> : BaseValidatorControlComponent<TControl>
        where TControl : BaseCompareValidator, new()
    {
        [Parameter]
        public ValidationDataType Type
        {
            get
            {
                return this.Control.Type;
            }
            set
            {
                this.Control.Type = value;
            }
        }

        [Parameter]
        public bool CultureInvariantValues
        {
            get
            {
                return this.Control.CultureInvariantValues;
            }
            set
            {
                this.Control.CultureInvariantValues = value;
            }
        }
    }
}
