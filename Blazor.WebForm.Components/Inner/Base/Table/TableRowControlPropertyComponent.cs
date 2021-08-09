using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    public abstract class TableRowControlPropertyComponent<T, TControl> : WebControlPropertyComponent<T, TControl>
        where TControl : System.Web.UI.WebControls.TableRow, new()
    {
        [Parameter]
        public System.Web.UI.WebControls.HorizontalAlign HorizontalAlign
        {
            get
            {
                return this.Control.HorizontalAlign;
            }
            set
            {
                this.Control.HorizontalAlign = value;
            }
        }

        [Parameter]
        public System.Web.UI.WebControls.TableRowSection TableSection
        {
            get
            {
                return this.Control.TableSection;
            }
            set
            {
                this.Control.TableSection = value;
            }
        }

        [Parameter]
        public System.Web.UI.WebControls.VerticalAlign VerticalAlign
        {
            get
            {
                return this.Control.VerticalAlign;
            }
            set
            {
                this.Control.VerticalAlign = value;
            }
        }
    }
}
