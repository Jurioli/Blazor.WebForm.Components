using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Blazor.WebForm.UI.ControlComponents;
using Extensions.ComponentModel;

namespace Microsoft.AspNetCore.Components
{
    public static class ControlComponentExtensions
    {
        public static void RequestRefresh<T>(this TemplateControlComponent<T> component) where T : TemplateControl, new()
        {
            component.LoadPostData();
            ControlComponentReflection<T>.SendMessageExtensionMethod(component, nameof(ControlComponentBase<Control>.RequestRefresh)
                , arguments: new object[] { component.Control });
        }
        private class ControlComponentReflection<T> where T : Control, new()
        {
            public delegate void SendMessageExtension(ControlComponent<T> component, string command, params object[] arguments);
            public static readonly SendMessageExtension SendMessageExtensionMethod = new ComponentOperator().ConvertToExtensionMethod<ControlComponent<T>, SendMessageExtension>("SendMessage", new Type[] { typeof(string), typeof(object[]) }, BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
