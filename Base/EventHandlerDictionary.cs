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

            public void Invoke(object sender, EventArgs e)
            {
                if (this.Handler is EventHandler handler)
                {
                    handler.Invoke(sender, e);
                }
            }

            public void Invoke<TEventArgs>(object sender, TEventArgs e)
            {
                if (this.Handler is EventHandler<TEventArgs> handler)
                {
                    handler.Invoke(sender, e);
                }
            }
        }

        private readonly ConcurrentDictionary<string, EventProperty> _events = new ConcurrentDictionary<string, EventProperty>();

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
                EventProperty eventProperty = _events.AddOrUpdate(propertyName, this.CreateEventProperty, this.UpdateEventProperty);
                if (eventProperty.Handler == null)
                {
                    add(eventProperty.Invoke);
                }
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
                EventProperty eventProperty = _events.AddOrUpdate(propertyName, this.CreateEventProperty, this.UpdateEventProperty);
                if (eventProperty.Handler == null)
                {
                    add(eventProperty.Invoke);
                }
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

        private EventProperty CreateEventProperty(string propertyName)
        {
            return new EventProperty();
        }

        private EventProperty UpdateEventProperty(string propertyName, EventProperty eventProperty)
        {
            return eventProperty;
        }
    }
}
