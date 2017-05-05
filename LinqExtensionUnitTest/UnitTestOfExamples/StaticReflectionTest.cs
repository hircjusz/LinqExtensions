using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{
    [TestClass]
    public class StaticReflectionTest
    {
        [TestMethod]
        public void GetMemberStringLength()
        {
            var Length = StaticReflection.GetMemberName<string>(x => x.Length);
            Assert.AreEqual(nameof(Length), Length);
        }

    }
}
