using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{
    [TestClass]
    public class LambdaExpressionTest
    {
        [TestMethod]
        public void MethodCallGetParameterName()
        {
            Expression<Func<int, int>> expr = a => a * a;
            var parameterExpression = expr.Parameters[0];
            Assert.AreEqual(parameterExpression.Name, "a");
        }

        [Ignore]
        public void MethodMultiplytBody()
        {
            Expression<Func<int, int>> expr = a => a * a;
            var bodyExpression = expr.Body;
            //Assert.IsNotNull(bodyExpression as Mul);
        }

        [TestMethod]
        public void MethodMethodCallExpression()
        {
            Expression<Func<string, bool>> expr = a => a.Contains("x");
            var bodyExpression = expr.Body;
            Assert.AreEqual(bodyExpression.NodeType, ExpressionType.Call);
            Assert.IsNotNull(bodyExpression as MethodCallExpression);
        }

        [TestMethod]
        public void MethodMemberAccessExpression()
        {
            Expression<Func<Person, string>> expr = a => a.Name;
            var bodyExpression = expr.Body;
            Assert.AreEqual(bodyExpression.NodeType, ExpressionType.MemberAccess);
            Assert.IsNotNull(bodyExpression as MemberExpression);
            Assert.AreEqual(expr.ReturnType, typeof(string));
            Assert.IsNotNull((bodyExpression as MemberExpression).Member.Name, "Name");
        }

        [TestMethod]
        public void ExpressionTreeAndAlsoTest1()
        {
            Expression<Func<Person, bool>> isTeenAgerExpr = s => s.Age > 12 && s.Age < 20;

            var pe = Expression.Parameter(typeof(Person));
            var ageProperty = Expression.Property(pe, "Age");
            var greaterThan = Expression.GreaterThan(ageProperty, Expression.Constant(12));
            var lessThan = Expression.LessThan(ageProperty, Expression.Constant(20));
            var andAlso = Expression.AndAlso(greaterThan, lessThan);
            var lambda = Expression.Lambda<Func<Person, bool>>(andAlso, pe);
            var func=lambda.Compile();

            var person10 = new Person() {Age = 10};
            var person15 = new Person() {Age = 10};
            var person20 = new Person() {Age = 10};

            var teenFunc = isTeenAgerExpr.Compile();
            Assert.AreEqual(teenFunc(person10),func(person10));
            Assert.AreEqual(teenFunc(person15),func(person15));
            Assert.AreEqual(teenFunc(person20),func(person20));
        }

    }
}