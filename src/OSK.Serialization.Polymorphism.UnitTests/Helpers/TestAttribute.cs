using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSK.Serialization.Polymorphism.UnitTests.Helpers
{
    public class TestAttribute : PolymorphismAttribute
    {
        public TestAttribute(string polymorphicPropertyName) 
            : base(polymorphicPropertyName)
        {
        }
    }
}
