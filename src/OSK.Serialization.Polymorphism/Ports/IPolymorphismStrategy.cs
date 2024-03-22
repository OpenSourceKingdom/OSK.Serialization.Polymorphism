using System;

namespace OSK.Serialization.Polymorphism.Ports
{
    public interface IPolymorphismStrategy
    {
        string GetPolymorphicPropertyName(Type typeToConvert);

        Type GetConcreteType(PolymorphismAttribute attribute, Type typeToConvert, object polymorphicPropertyValue);
    }
}
