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
        private class EventProperty
        {
            public object Handler { get; set; }
            public object BindHandler { get; set; }

            public void Invoke(object sender, EventArgs e)
            {
                if (this.BindHandler is EventHandler bindHandler)
                {
                    bindHandler.Invoke(sender, e);
                }
                if (this.Handler is EventHandler handler)
                {
                    handler.Invoke(sender, e);
                }
            }

            public void Invoke<TEventArgs>(object sender, TEventArgs e)
            {
                if (this.BindHandler is EventHandler<TEventArgs> bindHandler)
                {
                    bindHandler.Invoke(sender, e);
                }
                if (this.Handler is EventHandler<TEventArgs> handler)
                {
                    handler.Invoke(sender, e);
                }
            }
        }

        private readonly ConcurrentDictionary<string, EventProperty> _events = new ConcurrentDictionary<string, EventProperty>();

        public bool HasEventProperty(string propertyName)
        {
            return _events.ContainsKey(propertyName);
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
            if (handler != null)
            {
                EventProperty eventProperty = _events.AddOrUpdate(propertyName, this.CreateEventProperty, this.UpdateEventProperty, add);
                eventProperty.Handler = handler;
            }
            else
            {
                if (_events.TryRemove(propertyName, out EventProperty eventProperty))
                {
                    remove(eventProperty.Invoke);
                    eventProperty.Handler = null;
                }
            }
        }

        public void SetEventProperty<TEventArgs>(EventHandler<TEventArgs> handler, Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove, string propertyName)
        {
            if (handler != null)
            {
                EventProperty eventProperty = _events.AddOrUpdate(propertyName, this.CreateEventProperty, this.UpdateEventProperty, add);
                eventProperty.Handler = handler;
            }
            else
            {
                if (_events.TryRemove(propertyName, out EventProperty eventProperty))
                {
                    remove(eventProperty.Invoke);
                    eventProperty.Handler = null;
                }
            }
        }

        public void SetBindEventProperty(string propertyName, EventHandler bindHandler)
        {
            if (bindHandler != null && _events.TryGetValue(propertyName, out EventProperty eventProperty))
            {
                eventProperty.BindHandler = bindHandler;
            }
        }

        public void SetBindEventProperty<TEventArgs>(string propertyName, EventHandler<TEventArgs> bindHandler)
        {
            if (bindHandler != null && _events.TryGetValue(propertyName, out EventProperty eventProperty))
            {
                eventProperty.BindHandler = bindHandler;
            }
        }

        private EventProperty CreateEventProperty(string propertyName, Action<EventHandler> add)
        {
            EventProperty eventProperty = new EventProperty();
            add(eventProperty.Invoke);
            return eventProperty;
        }

        private EventProperty UpdateEventProperty(string propertyName, EventProperty eventProperty, Action<EventHandler> add)
        {
            return eventProperty;
        }

        private EventProperty CreateEventProperty<TEventArgs>(string propertyName, Action<EventHandler<TEventArgs>> add)
        {
            EventProperty eventProperty = new EventProperty();
            add(eventProperty.Invoke);
            return eventProperty;
        }

        private EventProperty UpdateEventProperty<TEventArgs>(string propertyName, EventProperty eventProperty, Action<EventHandler<TEventArgs>> add)
        {
            return eventProperty;
        }
    }
}
