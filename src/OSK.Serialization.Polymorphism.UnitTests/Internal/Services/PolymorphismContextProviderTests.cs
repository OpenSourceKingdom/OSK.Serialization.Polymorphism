using Moq;
using OSK.Serialization.Polymorphism.Internal.Services;
using OSK.Serialization.Polymorphism.Models;
using OSK.Serialization.Polymorphism.Ports;
using OSK.Serialization.Polymorphism.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OSK.Serialization.Polymorphism.UnitTests.Internal.Services
{
    public class PolymorphismContextProviderTests
    {
        #region Variables

        private readonly IList<PolymorphismStrategyDescriptor> _descriptors;
        private readonly Mock<IServiceProvider> _mockServiceProvider;
        private readonly IPolymorphismContextProvider _provider;

        #endregion

        #region Constructors

        public PolymorphismContextProviderTests()
        {
            _descriptors = new List<PolymorphismStrategyDescriptor>();
            _mockServiceProvider = new Mock<IServiceProvider>();

            _provider = new PolymorphismContextProvider(_descriptors, _mockServiceProvider.Object);
        }

        #endregion

        #region HasPolymorphismStrategy

        [Fact]
        public void HasPolymorphismStrategy_NullType_ReturnsFalse()
        {
            // Arrange/Act
            var result = _provider.HasPolymorphismStrategy(null);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(typeof(char))]
        [InlineData(typeof(byte))]
        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        [InlineData(typeof(double))]
        [InlineData(typeof(float))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(int?))]
        [InlineData(typeof(Type))]
        [InlineData(typeof(string))]
        [InlineData(typeof(Dictionary<int, int>))]
        [InlineData(typeof(List<string>))]
        [InlineData(typeof(TestAttribute))]
        [InlineData(typeof(object))]
        [InlineData(typeof(DateTime))]
        public void HasPolymorphismStrategy_TypeWithoutPolymorphismAttribute_ReturnsFalse(Type type)
        {
            // Arrange/Act
            var result = _provider.HasPolymorphismStrategy(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasPolymorphismStrategy_HasPolymorphismAttributeButNoStrategy_ReturnsFalse()
        {
            // Arrange/Act
            var result = _provider.HasPolymorphismStrategy(typeof(PolymorphicClass));

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasPolymorphismStrategy_HasPolymorphismAttributeAndStrategy_ReturnsTrue()
        {
            // Arrange
            _descriptors.Add(new PolymorphismStrategyDescriptor()
            {
                PolymorphicAttributeType = typeof(TestAttribute),
                PolymorphismStrategyType = typeof(object)
            });

            // Act
            var result = _provider.HasPolymorphismStrategy(typeof(PolymorphicClass));

            // Assert
            Assert.True(result);
        }

        #endregion

        #region GetPolymorphismContext

        [Fact]
        public void GetPolymorphismContext_NullType_ReturnsNullContext()
        {
            // Arrange/Act
            var context = _provider.GetPolymorphismContext(null);

            // Assert
            Assert.Null(context);
        }

        [Fact]
        public void GetPolymorphismContext_NoPolymorphismAttribute_ReturnsNullContext()
        {
            // Arrange/Act
            var context = _provider.GetPolymorphismContext(typeof(int));

            // Assert
            Assert.Null(context);
        }

        [Fact]
        public void GetPolymorphismContext_PolymorphismAttributeHasNoStrategy_ReturnsNullContext()
        {
            // Arrange/Act
            var context = _provider.GetPolymorphismContext(typeof(PolymorphicClass));

            // Assert
            Assert.Null(context);
        }

        [Fact]
        public void GetPolymorphismContext_PolymorphismAttributeHasStrategy_ReturnsContext()
        {
            // Arrange
            _descriptors.Add(new PolymorphismStrategyDescriptor()
            {
                PolymorphicAttributeType = typeof(TestAttribute),
                PolymorphismStrategyType = typeof(PolymorphicClass)
            });

            _mockServiceProvider.Setup(m => m.GetService(It.IsAny<Type>()))
                .Returns(Mock.Of<IPolymorphismStrategy>());

            // Act
            var context = _provider.GetPolymorphismContext(typeof(PolymorphicClass));

            // Assert
            Assert.NotNull(context);
            Assert.Equal(nameof(PolymorphicClass.TestProperty), context.PolymorphismPropertyName);
        }

        #endregion
    }
}
