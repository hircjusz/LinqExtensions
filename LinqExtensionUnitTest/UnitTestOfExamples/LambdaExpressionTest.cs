﻿using System;
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
            Assert.AreEqual(expr.ReturnType,typeof(string));
            Assert.IsNotNull((bodyExpression as MemberExpression).Member.Name, "Name");
        }




    }
}