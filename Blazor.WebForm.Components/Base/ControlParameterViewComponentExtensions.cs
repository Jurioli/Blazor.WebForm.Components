﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using IComponent = Microsoft.AspNetCore.Components.IComponent;

namespace Blazor.WebForm.UI
{
    public interface IControlParameterViewComponent : IParameterViewComponent
    {
        internal Control Control { get; }
        internal IReadOnlyDictionary<string, object> Attributes { get; set; }
        internal List<string> ReserveParameters { get; set; }
    }

    public interface IParameterViewComponent : IComponent
    {
        internal EventHandlerDictionary Events { get; }
    }

    public static class ControlParameterViewComponentExtensions
    {
        internal static void FilterParameters(this IControlParameterViewComponent component, ref ParameterView parameters)
        {
            List<string> reserveParameters = component.ReserveParameters;
            if (reserveParameters == null)
            {
                reserveParameters = component.InitReserveParameters(parameters);
                component.ReserveParameters = reserveParameters;
            }
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
            foreach (KeyValuePair<string, object> pair in parameters.ToDictionary())
            {
                if (reserveParameters.Contains(pair.Key))
                {
                    list.Add(pair);
                }
            }
            parameters = ParameterView.FromDictionary(new Dictionary<string, object>(list));
        }

        private static List<string> InitReserveParameters(this IControlParameterViewComponent component, ParameterView parameters)
        {
            List<string> reserveParameters = new List<string>();
            IReadOnlyDictionary<string, object> attributes = component.Attributes;
            foreach (KeyValuePair<string, object> pair in parameters.ToDictionary())
            {
                if (pair.Key == "ChildContent" || HasPropertyBindEvent(attributes, pair.Key) || IsRenderFragment(pair.Value))
                {
                    reserveParameters.Add(pair.Key);
                }
            }
            return reserveParameters;
        }

        private static bool HasPropertyBindEvent(IReadOnlyDictionary<string, object> attributes, string propertyName)
        {
            if (attributes != null && attributes.ContainsKey($"{propertyName}Changed"))
            {
                return true;
            }
            return false;
        }

        private static bool IsRenderFragment(object value)
        {
            if (value != null)
            {
                if (value is RenderFragment)
                {
                    return true;
                }
                Type type = value.GetType();
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(RenderFragment<>))
                {
                    return true;
                }
            }
            return false;
        }

        public static void SetAttributes(this IControlParameterViewComponent component, IReadOnlyDictionary<string, object> attributes)
        {
            if (attributes != null && component.Control is IAttributeAccessor accessor)
            {
                foreach (KeyValuePair<string, object> attribute in attributes)
                {
                    if (attribute.Value is string value)
                    {
                        accessor.SetAttribute(attribute.Key, value);
                    }
                }
            }
            component.Attributes = attributes;
        }

        public static EventHandler GetEventProperty(this IParameterViewComponent component, [CallerMemberName] string propertyName = null)
        {
            return component.Events.GetEventProperty(propertyName);
        }

        public static EventHandler<TEventArgs> GetEventProperty<TEventArgs>(this IParameterViewComponent component, [CallerMemberName] string propertyName = null)
        {
            return component.Events.GetEventProperty<TEventArgs>(propertyName);
        }

        public static void SetEventProperty(this IParameterViewComponent component, EventHandler handler, Action<EventHandler> add, Action<EventHandler> remove
            , [CallerMemberName] string propertyName = null)
        {
            component.Events.SetEventProperty(handler, add, remove, propertyName);
        }

        public static void SetEventProperty<TEventArgs>(this IParameterViewComponent component, EventHandler<TEventArgs> handler, Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove
            , [CallerMemberName] string propertyName = null)
        {
            component.Events.SetEventProperty(handler, add, remove, propertyName);
        }

        public static string ConvertToString<TValue>(this IParameterViewComponent component, TValue value)
        {
            return TypeDescriptor.GetConverter(typeof(TValue)).ConvertToString(null, CultureInfo.CurrentCulture, value);
        }

        public static TValue ConvertFromString<TValue>(this IParameterViewComponent component, string value)
        {
            return (TValue)TypeDescriptor.GetConverter(typeof(TValue)).ConvertFromString(null, CultureInfo.CurrentCulture, value);
        }

        public static string ConvertToString<TConverter, TValue>(this IParameterViewComponent component, TValue value)
            where TConverter : TypeConverter, new()
        {
            return new TConverter().ConvertToString(null, CultureInfo.CurrentCulture, value);
        }

        public static TValue ConvertFromString<TConverter, TValue>(this IParameterViewComponent component, string value)
            where TConverter : TypeConverter, new()
        {
            return (TValue)new TConverter().ConvertFromString(null, CultureInfo.CurrentCulture, value);
        }

        public static ITemplate GetTemplateProperty(this IParameterViewComponent component, RenderFragment value)
        {
            if (value != null)
            {
                return new RenderFragmentTemplateBuilder(value);
            }
            else
            {
                return null;
            }
        }

        public static ITemplate GetTemplateProperty<TValue>(this IParameterViewComponent component, RenderFragment<TValue> value)
        {
            if (value != null)
            {
                return new RenderFragmentBindableTemplateBuilder<TValue>(value, ExtractValues);
            }
            else
            {
                return null;
            }
        }

        public static ITemplate GetInsertTemplateProperty<TValue>(this IParameterViewComponent component, RenderFragment<TValue> value)
            where TValue : new()
        {
            if (value != null)
            {
                return new RenderFragmentBindableTemplateBuilder<TValue>(value, ExtractValues, () => new TValue());
            }
            else
            {
                return null;
            }
        }

        private static IOrderedDictionary ExtractValues<TValue>(Control container, TValue context)
        {
            return new OrderedDictionary().Apply(() => context);
        }
    }
}