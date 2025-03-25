using FieldTool.ClipboardLookup.Helpers;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FieldTool.ClipboardLookup.Resolver
{
    public class IgnorableContractResolver : DefaultContractResolver
    {
        protected readonly Dictionary<Type, List<string>> IgnoreProperties;

        public IgnorableContractResolver()
        {
            IgnoreProperties = new Dictionary<Type, List<string>>();
        }

        public void Ignore(Type type, params string[] propertyNames)
        {
            if (!IgnoreProperties.ContainsKey(type))
            {
                IgnoreProperties[type] = new List<string>();
            }

            IgnoreProperties[type].AddRange(propertyNames);
        }

        public void Ignore<T>(Expression<Func<T, object>> selector)
        {
            Ignore(typeof(T), DataHelper.GetPropertyName<T>(selector));
        }

        public bool IsIgnored(Type type, string propertyName)
        {
            return IgnoreProperties.ContainsKey(type) &&
                IgnoreProperties[type].Contains(propertyName);
        }

        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (IsIgnored(property.DeclaringType, property.PropertyName)
                || IsIgnored(property.DeclaringType.BaseType, property.PropertyName))
            {
                property.ShouldSerialize = instance => { return false; };
            }

            return property;
        }
    }
}