using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSK.Serialization.Polymorphism.UnitTests.Helpers
{
    [Test(polymorphicPropertyName: "TestProperty")]
    public class PolymorphicClass
    {
        public string TestProperty { get; set; }
    }
}
