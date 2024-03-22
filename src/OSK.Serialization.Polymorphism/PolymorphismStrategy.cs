using OSK.Serialization.Polymorphism.Ports;
using System;

namespace OSK.Serialization.Polymorphism
{
    public abstract class PolymorphismStrategy<TAttribute> : IPolymorphismStrategy
        where TAttribute : PolymorphismAttribute
    {
        #region IPolymorphismStrategy

        public Type GetConcreteType(PolymorphismAttribute attribute, Type typeToConvert, object polymorphicPropertyValue)
        {
            var polymorphicAttribute = attribute as TAttribute;
            if (polymorphicAttribute == null)
            {
                return typeToConvert;
            }
            if (typeToConvert == null)
            {
                return null;
            }
            if (polymorphicPropertyValue == null)
            {
                return null;
            }

            return GetConcreteType(polymorphicAttribute, typeToConvert, polymorphicPropertyValue);
        }

        public string GetPolymorphicPropertyName(Type typeToConvert)
        {
            if (typeToConvert == null)
            {
                return null;
            }

            var polymorphismAttribute = PolymorphismAttribute.GetPolymorphismAttribute(typeToConvert) as TAttribute;
            return polymorphismAttribute?.PolymorphicPropertyName;
        }

        #endregion

        #region Helpers

        protected abstract Type GetConcreteType(TAttribute attribute, Type typeToConvert, object propertyValue);

        #endregion
    }
}
