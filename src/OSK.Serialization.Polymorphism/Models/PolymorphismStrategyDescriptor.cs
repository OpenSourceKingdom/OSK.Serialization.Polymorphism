using System;

namespace OSK.Serialization.Polymorphism.Models
{
    public class PolymorphismStrategyDescriptor
    {
        public Type PolymorphismStrategyType { get; set; }

        public Type PolymorphicAttributeType { get; set; }
    }
}
