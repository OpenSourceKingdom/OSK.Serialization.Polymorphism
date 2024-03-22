using OSK.Serialization.Polymorphism.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OSK.Serialization.Polymorphism.UnitTests
{
    public class PolymorphismAttributeTests
    {
        #region GetPolymorphismAttribute

        [Theory]
        [InlineData(typeof(char), false)]
        [InlineData(typeof(byte), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(long), false)]
        [InlineData(typeof(double), false)]
        [InlineData(typeof(float), false)]
        [InlineData(typeof(decimal), false)]
        [InlineData(typeof(int?), false)]
        [InlineData(typeof(Type), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(Dictionary<int, int>), false)]
        [InlineData(typeof(List<string>), false)]
        [InlineData(typeof(TestAttribute), false)]
        [InlineData(typeof(object), false)]
        [InlineData(typeof(DateTime), false)]
        [InlineData(typeof(PolymorphicClass), true)]
        public void GetPolymorphismAttribute_DifferingTypes_ReturnsExpectedAttributeResult(Type type, bool expectedResult)
        {
            // Arrange/Act
            var attribute = PolymorphismAttribute.GetPolymorphismAttribute(type);

            // Assert
            if (expectedResult)
            {
                Assert.NotNull(attribute);
            }
            else
            {
                Assert.Null(attribute);
            }
        }

        [Fact]
        public void GetPolymorphismAttribute_HasPolymorphicAttribute_ReturnsExpectedAttribute()
        {
            // Arrange/Act
            var polymorphicAttribute = PolymorphismAttribute.GetPolymorphismAttribute(typeof(PolymorphicClass));

            // Assert
            Assert.NotNull(polymorphicAttribute);
            Assert.Equal(nameof(PolymorphicClass.TestProperty), polymorphicAttribute.PolymorphicPropertyName);
        }

        #endregion
    }
}
