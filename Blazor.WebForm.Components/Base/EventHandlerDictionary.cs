﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI
{
    internal class EventHandlerDictionary
    {
        private abstract class EventProperty
        {
            private readonly Func<bool> _inSetParams;

            public abstract MulticastDelegate Handler { get; set; }

            public abstract MulticastDelegate BindHandler { get; set; }

            public abstract bool IsEmpty { get; }

            public abstract bool IsBoth { get; }

            protected bool InSetParams
            {
                get
                {
                    return _inSetParams != null && _inSetParams.Invoke();
                }
            }

            public EventProperty(Func<bool> inSetParams)
            {
                _inSetParams = inSetParams;
            }

            public abstract void Add();

            public abstract void Remove();
        }

        private class GeneralEventProperty : EventProperty
        {
            private readonly Action<EventHandler> _add;
            private readonly Action<EventHandler> _remove;
            private EventHandler _handler;
            private EventHandler _bindHandler;
            private bool _invoking;

            public override MulticastDelegate Handler
            {
                get
                {
                    return _handler;
                }
                set
                {
                    _handler = value as EventHandler;
                }
            }

            public override MulticastDelegate BindHandler
            {
                get
                {
                    return _bindHandler;
                }
                set
                {
                    _bindHandler = value as EventHandler;
                }
            }

            public override bool IsEmpty
            {
                get
                {
                    return _handler == null && _bindHandler == null;
                }
            }

            public override bool IsBoth
            {
                get
                {
                    return _handler != null && _bindHandler != null;
                }
            }

            public GeneralEventProperty(Func<bool> inSetParams, Action<EventHandler> add, Action<EventHandler> remove)
                : base(inSetParams)
            {
                _add = add;
                _remove = remove;
            }

            public override void Add()
            {
                _add(this.Invoke);
            }

            public override void Remove()
            {
                _remove(this.Invoke);
            }

            private void Invoke(object sender, EventArgs e)
            {
                if (!_invoking)
                {
                    try
                    {
                        _invoking = true;
                        if (!this.InSetParams && _bindHandler != null)
                        {
                            _bindHandler.Invoke(sender, e);
                        }
                        else
                        {
                            _handler?.Invoke(sender, e);
                        }
                    }
                    finally
                    {
                        _invoking = false;
                    }
                }
            }
        }

        private class EventProperty<TEventArgs> : EventProperty
        {
            private readonly Action<EventHandler<TEventArgs>> _add;
            private readonly Action<EventHandler<TEventArgs>> _remove;
            private EventHandler<TEventArgs> _handler;
            private EventHandler<TEventArgs> _bindHandler;
            private bool _invoking;

            public override MulticastDelegate Handler
            {
                get
                {
                    return _handler;
                }
                set
                {
                    _handler = value as EventHandler<TEventArgs>;
                }
            }

            public override MulticastDelegate BindHandler
            {
                get
                {
                    return _bindHandler;
                }
                set
                {
                    _bindHandler = value as EventHandler<TEventArgs>;
                }
            }

            public override bool IsEmpty
            {
                get
                {
                    return _handler == null && _bindHandler == null;
                }
            }

            public override bool IsBoth
            {
                get
                {
                    return _handler != null && _bindHandler != null;
                }
            }

            public EventProperty(Func<bool> inSetParams, Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove)
                : base(inSetParams)
            {
                _add = add;
                _remove = remove;
            }

            public override void Add()
            {
                _add(this.Invoke);
            }

            public override void Remove()
            {
                _remove(this.Invoke);
            }

            private void Invoke(object sender, TEventArgs e)
            {
                if (!_invoking)
                {
                    try
                    {
                        _invoking = true;
                        if (!this.InSetParams && _bindHandler != null)
                        {
                            _bindHandler.Invoke(sender, e);
                        }
                        else
                        {
                            _handler?.Invoke(sender, e);
                        }
                    }
                    finally
                    {
                        _invoking = false;
                    }
                }
            }
        }

        private readonly ConcurrentDictionary<string, EventProperty> _events = new ConcurrentDictionary<string, EventProperty>();
        private readonly Func<bool> _inSetParams;

        public EventHandlerDictionary(Func<bool> inSetParams)
        {
            _inSetParams = inSetParams;
        }

        public void RemoveAll()
        {
            if (!_events.IsEmpty)
            {
                foreach (EventProperty eventProperty in _events.Values)
                {
                    eventProperty.Remove();
                }
                _events.Clear();
            }
        }

        public EventHandler GetEventProperty(string propertyName)
        {
            if (_events.TryGetValue(propertyName, out EventProperty eventProperty))
            {
                return (EventHandler)eventProperty.Handler;
            }
            return null;
        }

        public EventHandler<TEventArgs> GetEventProperty<TEventArgs>(string propertyName)
        {
            if (_events.TryGetValue(propertyName, out EventProperty eventProperty))
            {
                return (EventHandler<TEventArgs>)eventProperty.Handler;
            }
            return null;
        }

        public void SetEventProperty(EventHandler handler, Action<EventHandler> add, Action<EventHandler> remove, string propertyName)
        {
            this.SetEventProperty(handler, this.CreateEventProperty, (add, remove), propertyName);
        }

        public void SetEventProperty<TEventArgs>(EventHandler<TEventArgs> handler, Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove, string propertyName)
        {
            this.SetEventProperty(handler, this.CreateEventProperty, (add, remove), propertyName);
        }

        private void SetEventProperty<TArg>(MulticastDelegate handler, Func<string, TArg, EventProperty> eventPropertyFactory, TArg factoryArgument, string propertyName)
        {
            EventProperty eventProperty;
            if (handler != null)
            {
                eventProperty = _events.GetOrAdd(propertyName, eventPropertyFactory, factoryArgument);
                eventProperty.Handler = handler;
            }
            else if (_events.TryGetValue(propertyName, out eventProperty))
            {
                eventProperty.Handler = null;
                if (eventProperty.IsEmpty && _events.TryRemove(propertyName, out eventProperty))
                {
                    eventProperty.Remove();
                }
            }
        }

        public void SetBindEventProperty(string propertyName, EventHandler bindHandler, Action<EventHandler> add, Action<EventHandler> remove)
        {
            this.SetBindEventProperty(propertyName, bindHandler, this.CreateEventProperty, (add, remove));
        }

        public void SetBindEventProperty<TEventArgs>(string propertyName, EventHandler<TEventArgs> bindHandler, Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove)
        {
            this.SetBindEventProperty(propertyName, bindHandler, this.CreateEventProperty, (add, remove));
        }

        private void SetBindEventProperty<TArg>(string propertyName, MulticastDelegate bindHandler, Func<string, TArg, EventProperty> eventPropertyFactory, TArg factoryArgument)
        {
            EventProperty eventProperty;
            if (bindHandler != null)
            {
                eventProperty = _events.GetOrAdd(propertyName, eventPropertyFactory, factoryArgument);
                eventProperty.BindHandler = bindHandler;
            }
            else if (_events.TryGetValue(propertyName, out eventProperty))
            {
                eventProperty.BindHandler = null;
                if (eventProperty.IsEmpty && _events.TryRemove(propertyName, out eventProperty))
                {
                    eventProperty.Remove();
                }
            }
        }

        private EventProperty CreateEventProperty(string propertyName, (Action<EventHandler> add, Action<EventHandler> remove) args)
        {
            EventProperty eventProperty = new GeneralEventProperty(_inSetParams, args.add, args.remove);
            eventProperty.Add();
            return eventProperty;
        }

        private EventProperty CreateEventProperty<TEventArgs>(string propertyName, (Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove) args)
        {
            EventProperty eventProperty = new EventProperty<TEventArgs>(_inSetParams, args.add, args.remove);
            eventProperty.Add();
            return eventProperty;
        }

        public bool TryGetEventCallbackAdapter<TEventArgs, TValue>(string propertyName, EventCallback<TValue> callback, out EventCallbackAdapter<TEventArgs, TValue> callbackAdapter)
        {
            if (_events.TryGetValue(propertyName, out EventProperty eventProperty) && eventProperty.IsBoth)
            {
                callbackAdapter = new EventCallbackAdapter<TEventArgs, TValue>(callback, eventProperty.Handler);
                return true;
            }
            callbackAdapter = null;
            return false;
        }
    }
}