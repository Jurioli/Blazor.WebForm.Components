using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Extensions.ComponentModel;

namespace Microsoft.AspNetCore.Components
{
    public static class ControlComponentExtensions
    {
        public static void RequestRefresh<T>(this ControlComponent<T> component) where T : Control, new()
        {
            component.LoadPostData();
            ControlComponentReflection<T>.SendMessageExtensionMethod(component, "RequestRefresh");
        }
        private class ControlComponentReflection<T> where T : Control, new()
        {
            public delegate void SendMessageExtension(ControlComponent<T> component, string command, params object[] arguments);
            public static SendMessageExtension SendMessageExtensionMethod = new ComponentOperator().ConvertToExtensionMethod<ControlComponent<T>, SendMessageExtension>("SendMessage", new Type[] { typeof(string), typeof(object[]) }, BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
