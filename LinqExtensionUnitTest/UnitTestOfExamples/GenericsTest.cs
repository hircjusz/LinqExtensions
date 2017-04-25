using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{
    [TestClass]
    public class GenericsTest
    {

        [TestMethod]
        public void MakeGeneric()
        {
            Type typeDict = typeof(Dictionary<,>);
            Type[] typeArgs = { typeof(string), typeof(string) };
            Type genericType = typeDict.MakeGenericType(typeArgs);
            IDictionary d = Activator.CreateInstance(genericType) as IDictionary;
            Assert.AreEqual(typeof(Dictionary<string,string>), genericType);
        }
    }
}
