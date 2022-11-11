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
            public abstract object Handler { get; set; }

            public abstract object BindHandler { get; set; }

            public abstract void Add();

            public abstract void Remove();
        }

        private class GeneralEventProperty : EventProperty
        {
            private readonly Action<EventHandler> _add;
            private readonly Action<EventHandler> _remove;
            private EventHandler _handler;
            private EventHandler _bindHandler;

            public override object Handler
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

            public override object BindHandler
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

            public GeneralEventProperty(Action<EventHandler> add, Action<EventHandler> remove) : base()
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
                _bindHandler?.Invoke(sender, e);
                _handler?.Invoke(sender, e);
            }
        }

        private class EventProperty<TEventArgs> : EventProperty
        {
            private readonly Action<EventHandler<TEventArgs>> _add;
            private readonly Action<EventHandler<TEventArgs>> _remove;
            private EventHandler<TEventArgs> _handler;
            private EventHandler<TEventArgs> _bindHandler;

            public override object Handler
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

            public override object BindHandler
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

            public EventProperty(Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove)
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
                _bindHandler?.Invoke(sender, e);
                _handler?.Invoke(sender, e);
            }
        }

        private readonly ConcurrentDictionary<string, EventProperty> _events = new ConcurrentDictionary<string, EventProperty>();

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
                EventProperty eventProperty = _events.AddOrUpdate(propertyName, this.CreateEventProperty, this.UpdateEventProperty, (add, remove));
                eventProperty.Handler = handler;
            }
            else
            {
                if (_events.TryRemove(propertyName, out EventProperty eventProperty))
                {
                    eventProperty.Remove();
                    eventProperty.Handler = null;
                }
            }
        }

        public void SetEventProperty<TEventArgs>(EventHandler<TEventArgs> handler, Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove, string propertyName)
        {
            if (handler != null)
            {
                EventProperty eventProperty = _events.AddOrUpdate(propertyName, this.CreateEventProperty, this.UpdateEventProperty, (add, remove));
                eventProperty.Handler = handler;
            }
            else
            {
                if (_events.TryRemove(propertyName, out EventProperty eventProperty))
                {
                    eventProperty.Remove();
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

        private EventProperty CreateEventProperty(string propertyName, (Action<EventHandler> add, Action<EventHandler> remove) args)
        {
            EventProperty eventProperty = new GeneralEventProperty(args.add, args.remove);
            eventProperty.Add();
            return eventProperty;
        }

        private EventProperty UpdateEventProperty(string propertyName, EventProperty eventProperty, (Action<EventHandler> add, Action<EventHandler> remove) args)
        {
            return eventProperty;
        }

        private EventProperty CreateEventProperty<TEventArgs>(string propertyName, (Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove) args)
        {
            EventProperty eventProperty = new EventProperty<TEventArgs>(args.add, args.remove);
            eventProperty.Add();
            return eventProperty;
        }

        private EventProperty UpdateEventProperty<TEventArgs>(string propertyName, EventProperty eventProperty, (Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove) args)
        {
            return eventProperty;
        }
    }
}
