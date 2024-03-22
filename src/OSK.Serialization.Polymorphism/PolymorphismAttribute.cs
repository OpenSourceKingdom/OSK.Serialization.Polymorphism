using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace OSK.Serialization.Polymorphism
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public abstract class PolymorphismAttribute : Attribute
    {
        #region Variables

        private static readonly ConcurrentDictionary<Type, PolymorphismAttribute> PolymorphismAttributeLookup = new ConcurrentDictionary<Type, PolymorphismAttribute>();

        public string PolymorphicPropertyName { get; }

        #endregion

        #region Constructors

        protected PolymorphismAttribute(string polymorphicPropertyName)
        {
            if (string.IsNullOrEmpty(polymorphicPropertyName))
            {
                throw new ArgumentNullException(nameof(polymorphicPropertyName));
            }

            PolymorphicPropertyName = polymorphicPropertyName;
        }

        #endregion

        #region Helpers

        public static PolymorphismAttribute GetPolymorphismAttribute(Type objectType)
        {
            if (!objectType.IsClass && !objectType.IsInterface && !objectType.IsAbstract)
            {
                return null;
            }

            return PolymorphismAttributeLookup.GetOrAdd(objectType,
                type => type.GetCustomAttribute<PolymorphismAttribute>(inherit: false));
        }

        #endregion
    }
}
