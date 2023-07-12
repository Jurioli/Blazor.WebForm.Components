using Extensions.ComponentModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI
{
    internal class EventCallbackAdapter<TValue>
    {
        private static readonly Getter<EventCallback<TValue>, MulticastDelegate> GetDelegate = new ComponentOperator().GetFieldGetter<EventCallback<TValue>, MulticastDelegate>("Delegate");
        private static readonly Getter<EventCallback<TValue>, IHandleEvent> GetReceiver = new ComponentOperator().GetFieldGetter<EventCallback<TValue>, IHandleEvent>("Receiver");

        private readonly EventCallback<TValue> _callback;
        private readonly MulticastDelegate _eventHandler;

        private MulticastDelegate Delegate
        {
            get
            {
                return GetDelegate(_callback);
            }
        }

        private IHandleEvent Receiver
        {
            get
            {
                return GetReceiver(_callback);
            }
        }

        public EventCallbackAdapter(EventCallback<TValue> callback, MulticastDelegate eventHandler)
        {
            _callback = callback;
            _eventHandler = eventHandler;
        }

        public Task InvokeAsync(TValue value, params object[] eventArgs)
        {
            EventCallbackWorkItem item = new EventCallbackWorkItem(this.DelegateInvoke);
            object arg = (value, eventArgs);
            IHandleEvent receiver = this.Receiver;
            return receiver != null ? receiver.HandleEventAsync(item, arg) : item.InvokeAsync(arg);
        }

        private void DelegateInvoke((TValue value, object[] eventArgs) arg)
        {
            this.Delegate?.DynamicInvoke(arg.value);
            _eventHandler?.DynamicInvoke(arg.eventArgs);
        }
    }
}