using OSK.Serialization.Polymorphism.Models;
using System;

namespace OSK.Serialization.Polymorphism.Ports
{
    public interface IPolymorphismContextProvider
    {
        bool HasPolymorphismStrategy(Type typeToConvert);

        PolymorphismContext GetPolymorphismContext(Type typeToConvert);
    }
}
