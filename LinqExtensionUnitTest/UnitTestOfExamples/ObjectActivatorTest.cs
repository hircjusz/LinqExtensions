using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{

    //public static class ObjectActivator<T> GetActivator

    [TestClass]
    public class ObjectActivatorTest
    {

        [TestMethod]
        public void SimpleNewExpressionToCreateType()
        {

            var exp = Expression.New(typeof(Person));
            var lambda = LambdaExpression.Lambda(exp);
            object myObj = lambda.Compile().DynamicInvoke();

            Assert.IsNotNull(myObj);
            Assert.IsInstanceOfType(myObj, typeof(Person));
        }


    }
}
