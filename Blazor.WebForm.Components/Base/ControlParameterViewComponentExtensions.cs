﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        internal ICollection<string> ReserveParameters { get; set; }
    }

    public interface IParameterViewComponent : IComponent
    {
        internal EventHandlerDictionary Events { get; }
    }

    public static class ControlParameterViewComponentExtensions
    {
        internal static IReadOnlyDictionary<string, object> FilterParameters(this IControlParameterViewComponent component, ParameterViewContext context)
        {
            IReadOnlyDictionary<string, object> parameters = context.Parameters;
            if (parameters.Count == 0)
            {
                return parameters;
            }
            ICollection<string> reserveParameters = component.ReserveParameters;
            if (reserveParameters == null)
            {
                reserveParameters = component.InitReserveParameters(parameters);
                component.ReserveParameters = reserveParameters;
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> value in parameters)
            {
                if (reserveParameters.Contains(value.Key))
                {
                    result.Add(value.Key, value.Value);
                }
            }
            context.UpdateParameters(result);
            return result;
        }

        private static ICollection<string> InitReserveParameters(this IControlParameterViewComponent component, IReadOnlyDictionary<string, object> parameters)
        {
            List<string> reserveParameters = new List<string>();
            foreach (KeyValuePair<string, object> value in parameters)
            {
                if (value.Key == "ChildContent" || parameters.HasPropertyBindEvent(value.Key) || IsRenderFragment(value.Value))
                {
                    reserveParameters.Add(value.Key);
                }
            }
            return reserveParameters;
        }

        internal static TControl CaptureReferenceControl<TControl>(this IControlParameterViewComponent component, Expression<Func<TControl>> func)
            where TControl : Control
        {
            if (func.Body is MemberExpression expression)
            {
                if (expression.Member is FieldInfo field)
                {
                    if (!field.IsStatic
                        && expression.Expression is ConstantExpression constant
                        && field.GetValue(constant.Value) == null)
                    {
                        field.SetValue(constant.Value, component.Control);
                    }
                }
                else if (expression.Member is PropertyInfo property)
                {
                    if (property.CanRead && property.CanWrite
                        && expression.Expression is ConstantExpression constant
                        && property.GetValue(constant.Value) == null)
                    {
                        property.SetValue(constant.Value, component.Control);
                    }
                }
            }
            return func.Compile().Invoke();
        }

        private static bool HasPropertyBindEvent(this IReadOnlyDictionary<string, object> parameters, string propertyName)
        {
            if (parameters.TryGetValue($"{propertyName}Changed", out object value) && value != null)
            {
                Type type = value.GetType();
                if (type.IsValueType && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(EventCallback<>))
                {
                    return true;
                }
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

        public static void SetBindEventProperty(this IParameterViewComponent component, string propertyName, EventHandler bindHandler, Action<EventHandler> add, Action<EventHandler> remove)
        {
            component.Events.SetBindEventProperty(propertyName, bindHandler, add, remove);
        }

        public static void SetBindEventProperty<TEventArgs>(this IParameterViewComponent component, string propertyName, EventHandler<TEventArgs> bindHandler, Action<EventHandler<TEventArgs>> add, Action<EventHandler<TEventArgs>> remove)
        {
            component.Events.SetBindEventProperty(propertyName, bindHandler, add, remove);
        }

        public static string ConvertToString<TValue>(this IParameterViewComponent component, TValue value)
        {
            return TypeDescriptor.GetConverter(typeof(TValue)).ConvertToInvariantString(value);
        }

        public static TValue ConvertFromString<TValue>(this IParameterViewComponent component, string value)
        {
            return (TValue)TypeDescriptor.GetConverter(typeof(TValue)).ConvertFromInvariantString(value);
        }

        public static string ConvertToString<TConverter, TValue>(this IParameterViewComponent component, TValue value)
            where TConverter : TypeConverter, new()
        {
            return new TConverter().ConvertToInvariantString(value);
        }

        public static TValue ConvertFromString<TConverter, TValue>(this IParameterViewComponent component, string value)
            where TConverter : TypeConverter, new()
        {
            return (TValue)new TConverter().ConvertFromInvariantString(value);
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

        public static ITemplate GetEditTemplateProperty<TValue>(this IParameterViewComponent component, RenderFragment<TValue> value)
            where TValue : class, new()
        {
            if (value != null)
            {
                return new RenderFragmentBindableTemplateBuilder<TValue>(value, ExtractValues, context => new TValue().Apply(() => context));
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