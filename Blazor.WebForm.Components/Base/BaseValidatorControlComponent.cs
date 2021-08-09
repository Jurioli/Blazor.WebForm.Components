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
    public abstract class BaseValidatorControlComponent<TControl> : LabelControlComponent<TControl>
        where TControl : BaseValidator, new()
    {
        [Parameter]
        public bool SetFocusOnError
        {
            get
            {
                return this.Control.SetFocusOnError;
            }
            set
            {
                this.Control.SetFocusOnError = value;
            }
        }

        [Parameter]
        public ValidatorDisplay Display
        {
            get
            {
                return this.Control.Display;
            }
            set
            {
                this.Control.Display = value;
            }
        }

        [Parameter]
        public bool EnableClientScript
        {
            get
            {
                return this.Control.EnableClientScript;
            }
            set
            {
                this.Control.EnableClientScript = value;
            }
        }

        [Parameter]
        public string ErrorMessage
        {
            get
            {
                return this.Control.ErrorMessage;
            }
            set
            {
                this.Control.ErrorMessage = value;
            }
        }

        [Parameter]
        public string ControlToValidate
        {
            get
            {
                return this.Control.ControlToValidate;
            }
            set
            {
                this.Control.ControlToValidate = value;
            }
        }

        [Parameter]
        public string ValidationGroup
        {
            get
            {
                return this.Control.ValidationGroup;
            }
            set
            {
                this.Control.ValidationGroup = value;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ValidatorCollection validators = (this.Control as IValidatePage).Validators;
            if (!validators.Contains(this.Control))
            {
                validators.Add(this.Control);
            }
        }

        protected override void SetInnerPropertyWithInner(IReadOnlyDictionary<string, object> parameters, ref bool hasInnerContent)
        {
            base.SetInnerPropertyWithInner(parameters, ref hasInnerContent);
            if (!parameters.ContainsKey(nameof(this.ID)) && string.IsNullOrEmpty(this.ID))
            {
                this.ID = Guid.NewGuid().ToString("N");
            }
        }
    }
}
