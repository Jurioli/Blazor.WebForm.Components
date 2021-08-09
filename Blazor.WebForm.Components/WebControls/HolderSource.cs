using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blazor.WebForm.UI
{
    public class HolderSource : Extensions.Web.UI.WebControls.HolderSource, IDataItemContainer
    {
        public object DataItem { get; set; }
        public int DataItemIndex { get; }
        public int DisplayIndex { get; }
    }
}
