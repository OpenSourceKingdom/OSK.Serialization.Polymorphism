using OSK.Serialization.Polymorphism.Ports;
using System;

namespace OSK.Serialization.Polymorphism.Models
{
    public class PolymorphismContext
    {
        #region Variables

        public string PolymorphismPropertyName => _attribute.PolymorphicPropertyName;

        private readonly IPolymorphismStrategy _strategy;
        private readonly PolymorphismAttribute _attribute;
        private readonly Type _typeToConvert;

        #endregion

        #region Constructors

        public PolymorphismContext(PolymorphismAttribute attribute, Type typeToConvert, IPolymorphismStrategy strategy)
        {
            _attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            _typeToConvert = typeToConvert ?? throw new ArgumentNullException(nameof(typeToConvert));
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        #endregion

        #region Helpers

        public bool IsStrategyOfType<TPolymorphismStrategy>()
            where TPolymorphismStrategy : IPolymorphismStrategy
        {
            return _strategy is TPolymorphismStrategy;
        }

        public Type GetConcreteType(object polymorphismPropertyValue)
        {
            return _strategy.GetConcreteType(_attribute, _typeToConvert, polymorphismPropertyValue);
        }

        #endregion
    }
}
