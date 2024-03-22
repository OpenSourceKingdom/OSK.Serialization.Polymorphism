using Microsoft.Extensions.DependencyInjection;
using OSK.Serialization.Polymorphism.Models;
using OSK.Serialization.Polymorphism.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Serialization.Polymorphism.Internal.Services
{
    internal class PolymorphismContextProvider : IPolymorphismContextProvider
    {
        #region Variables

        private readonly IEnumerable<PolymorphismStrategyDescriptor> _polymorphismStrategyDescriptors;
        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructors

        public PolymorphismContextProvider(IEnumerable<PolymorphismStrategyDescriptor> polymorphismStrategyDescriptors,
            IServiceProvider serviceProvider)
        {
            _polymorphismStrategyDescriptors = polymorphismStrategyDescriptors ?? throw new ArgumentNullException(nameof(polymorphismStrategyDescriptors));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #endregion

        #region IPolymorphismContextProvider

        public bool HasPolymorphismStrategy(Type typeToConvert)
        {
            if (typeToConvert == null)
            {
                return false;
            }

            var polymorphismAttribute = PolymorphismAttribute.GetPolymorphismAttribute(typeToConvert);
            if (polymorphismAttribute == null)
            {
                return false;
            }

            return _polymorphismStrategyDescriptors.Any(descriptor => descriptor.PolymorphicAttributeType == polymorphismAttribute.GetType());
        }

        public PolymorphismContext GetPolymorphismContext(Type typeToConvert)
        {
            if (typeToConvert == null)
            {
                return null;
            }

            var polymorphismAttribute = PolymorphismAttribute.GetPolymorphismAttribute(typeToConvert);
            if (polymorphismAttribute == null)
            {
                return null;
            }

            var polymorphismStrategyDescriptor = _polymorphismStrategyDescriptors.FirstOrDefault(descriptor
                => descriptor.PolymorphicAttributeType == polymorphismAttribute.GetType());
            if (polymorphismStrategyDescriptor == null)
            {
                return null;
            }

            var strategy = (IPolymorphismStrategy)_serviceProvider.GetRequiredService(polymorphismStrategyDescriptor.PolymorphismStrategyType);
            return new PolymorphismContext(polymorphismAttribute, typeToConvert, strategy);
        }

        #endregion
    }
}
