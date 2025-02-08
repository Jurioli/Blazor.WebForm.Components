using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI
{
    internal class EventCallbackAdapter<TEventArgs, TValue>
    {
        [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "Delegate")]
        private static extern ref MulticastDelegate GetDelegate(ref EventCallback<TValue> callback);
        [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "Receiver")]
        private static extern ref IHandleEvent GetReceiver(ref EventCallback<TValue> callback);
        [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "InvokeAsync")]
        private static extern Task InvokeAsync<T>(EventCallbackWorkItem item, MulticastDelegate @delegate, T arg);

        private readonly Lazy<Func<object, Task>> _delegateInvoke;

        public EventCallbackAdapter(EventCallback<TValue> callback, MulticastDelegate eventHandler)
        {
            _delegateInvoke = new Lazy<Func<object, Task>>(() =>
            {
                EventCallbackWorkItem item = GetItem(GetDelegate(ref callback), eventHandler);
                IHandleEvent receiver = GetReceiver(ref callback);
                if (receiver != null)
                {
                    return arg => receiver.HandleEventAsync(item, arg);
                }
                else
                {
                    return item.InvokeAsync;
                }
            });
        }

        public Task InvokeAsync(TValue value, object sender, TEventArgs e)
        {
            return _delegateInvoke.Value.Invoke((value, sender, e));
        }

        private static EventCallbackWorkItem GetItem(MulticastDelegate @delegate, MulticastDelegate eventHandler)
        {
            Func<object, TEventArgs, Task> invokeAsync = GetInvokeAsync(eventHandler);
            return new EventCallbackWorkItem(async (object arg) =>
            {
                (TValue value, object sender, TEventArgs e) = ((TValue, object, TEventArgs))arg;
                await InvokeAsync(default(EventCallbackWorkItem), @delegate, value);
                await invokeAsync.Invoke(sender, e);
            });
        }

        private static Func<object, TEventArgs, Task> GetInvokeAsync(MulticastDelegate eventHandler)
        {
            return eventHandler switch
            {
                null => (sender, e) => Task.CompletedTask,
                EventHandler handler => (sender, e) =>
                {
                    handler.Invoke(sender, e as EventArgs);
                    return Task.CompletedTask;
                }
                ,
                EventHandler<TEventArgs> handler => (sender, e) =>
                {
                    handler.Invoke(sender, e);
                    return Task.CompletedTask;
                }
                ,
                _ => (sender, e) =>
                {
                    try
                    {
                        return eventHandler.DynamicInvoke(sender, e) as Task ?? Task.CompletedTask;
                    }
                    catch (TargetInvocationException ex)
                    {
                        return Task.FromException(ex.InnerException);
                    }
                }
            };
        }
    }
}
