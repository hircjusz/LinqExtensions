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

        [TestMethod]
        public void CallContainsParameterInput()
        {
            var values = new List<string>() { "PETR", "VALE" };
            var type = typeof(string);
            var parameterExp = Expression.Parameter(type, "");
            var someValue = Expression.Constant(values, typeof(IEnumerable<string>));
            var containsMethodExp = Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(string) }, someValue, parameterExp);
            var lamda = Expression.Lambda<Func<string, bool>>(containsMethodExp, parameterExp);

            //ARRANGE
            var flag=lamda.Compile()("PETR");
            Assert.IsTrue(flag);
            var flag2 = lamda.Compile()("PETR2");
            Assert.IsFalse(flag2);
        }

    }
}
