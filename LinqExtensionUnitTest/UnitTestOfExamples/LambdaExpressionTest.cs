using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{
    [TestClass]
    public class LambdaExpressionTest
    {
        [TestMethod]
        public void MethodCallGetParametersName()
        {

            Expression<Func<int,int>> expr = a => a*a;



        }
    }
}