﻿using Blazor.WebForm.UI.PropertyComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    [CommonPropertyAdapter(typeof(System.Web.UI.WebControls.ListView), typeof(ListViewEmptyDataTemplate))]
    public partial class EmptyDataTemplate
    {
        protected class ListViewEmptyDataTemplate : TemplatePropertyComponentAdapter<EmptyDataTemplate, System.Web.UI.WebControls.ListView>
        {

        }
    }
}