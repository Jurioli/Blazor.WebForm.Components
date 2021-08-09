using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI.PropertyComponents
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommonPropertyAdapterAttribute : Attribute
    {
        private readonly Type _ownerType;
        private readonly Type _adapterType;
        public Type OwnerType
        {
            get
            {
                return _ownerType;
            }
        }
        public Type AdapterType
        {
            get
            {
                return _adapterType;
            }
        }
        public CommonPropertyAdapterAttribute(Type ownerType, Type adapterType)
        {
            _ownerType = ownerType;
            _adapterType = adapterType;
        }
    }
    internal class CommonPropertyAdapterMapper
    {
        private static ConcurrentDictionary<Type, ConcurrentDictionary<Type, Type>> adapterTypeMapper = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, Type>>();
        public static PropertyComponentAdapter<T> GetAdapter<T>(PropertyComponent<T> component)
        {
            Type type = component.GetType();
            Type ownerType = component.Owner.GetType();
            if (!adapterTypeMapper.TryGetValue(type, out ConcurrentDictionary<Type, Type> adapterTypes))
            {
                object[] attributes = type.GetCustomAttributes(typeof(CommonPropertyAdapterAttribute), true);
                if (attributes.Length > 0)
                {
                    ConcurrentDictionary<Type, Type> dictionary = new ConcurrentDictionary<Type, Type>();
                    foreach (CommonPropertyAdapterAttribute attribute in attributes)
                    {
                        if (attribute.OwnerType != null && typeof(T).IsAssignableFrom(attribute.OwnerType) &&
                            attribute.AdapterType != null && typeof(PropertyComponentAdapter<T>).IsAssignableFrom(attribute.AdapterType))
                        {
                            dictionary.TryAdd(attribute.OwnerType, attribute.AdapterType);
                        }
                    }
                    adapterTypes = dictionary;
                }
                adapterTypeMapper.TryAdd(type, adapterTypes);
            }
            if (adapterTypes == null)
            {
                return null;
            }
            Type adapterType, tempType = ownerType;
            while (true)
            {
                if (adapterTypes.TryGetValue(tempType, out adapterType))
                {
                    if (tempType != ownerType)
                    {
                        adapterTypes.TryAdd(ownerType, adapterType);
                    }
                    break;
                }
                tempType = tempType.BaseType;
                if (tempType == null)
                {
                    return null;
                }
            }
            if (type.IsGenericType)
            {
                Type[] typeArguments = type.GetGenericArguments();
                adapterType = adapterType.MakeGenericType(typeArguments);
            }
            PropertyComponentAdapter<T> adapter = (PropertyComponentAdapter<T>)Activator.CreateInstance(adapterType);
            adapter.Component = component;
            return adapter;
        }
    }
}
