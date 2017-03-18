using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{
    [TestClass]
    public class UnitTestOfExamples
    {
        [TestMethod]
        public void MultiplyExamples()
        {

            var exp1 = Expression.Parameter(typeof(int), "a");
            var exp2 = Expression.Parameter(typeof(int), "b");
            var exp = Expression.Multiply(exp1, exp2);
            var lambdaExpr = Expression.Lambda<Func<int, int, int>>(exp, exp1, exp2);
            var result = lambdaExpr.Compile().Invoke(2, 5);
            Assert.AreEqual(10, result);
        }
    }
}
