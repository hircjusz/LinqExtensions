using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{
    [TestClass]
    public class LambdaExpressionTest
    {
        [TestMethod]
        public void MethodCallGetParameterName()
        {
            Expression<Func<int,int>> expr = a => a*a;
            var parameterExpression=expr.Parameters[0];
            Assert.AreEqual(parameterExpression.Name,"a");
        }

        [Ignore]
        public void MethodMultiplytBody()
        {
            Expression<Func<int, int>> expr = a => a * a;
            var bodyExpression = expr.Body;
          //  Assert.AreEqual(bodyExpression.GetType(),typeof(System.Linq.Expressions.SimpleBin));
        }

    }
}