using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{
    [TestClass]
    public class MethodCallExpressionTest
    {
        [TestMethod]
        public void CallUpper()
        {
            var sampleString = "sample string";
            Expression callExpr = Expression.Call(Expression.Constant(sampleString), typeof(String).GetMethod("ToUpper", new Type[] { }));
            var lambda = Expression.Lambda<Func<String>>(callExpr).Compile();
            Assert.AreEqual(sampleString.ToUpper(), lambda.DynamicInvoke());
        }


    }
}
