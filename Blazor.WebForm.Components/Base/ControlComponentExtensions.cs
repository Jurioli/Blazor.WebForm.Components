using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Blazor.WebForm.UI.ControlComponents;

namespace Microsoft.AspNetCore.Components
{
    public static class ControlComponentExtensions
    {
        public static void RequestRefresh<T>(this TemplateControlComponent<T> component) where T : TemplateControl, new()
        {
            component.LoadPostData();
            ControlComponentReflection<T>.SendMessage(component, nameof(ControlComponentBase<Control>.RequestRefresh)
                , arguments: new object[] { component.Control });
        }
        private class ControlComponentReflection<T> where T : Control, new()
        {
            [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "SendMessage")]
            public static extern void SendMessage(ControlComponent<T> component, string command, params object[] arguments);
        }
    }
}
