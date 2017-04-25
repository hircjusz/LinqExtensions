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
            Assert.AreEqual(expr.ReturnType,typeof(string));
            Assert.IsNotNull((bodyExpression as MemberExpression).Member.Name, "Name");
        }


        [TestMethod]
        public void MethodLambdaQueryableExpression()
        {

            Type typeDict = typeof(Dictionary<,>);

            //Creating KeyValue Type for Dictionary.
            Type[] typeArgs = { typeof(string), typeof(string) };

            //Passing the Type and create Dictionary Type.
            Type genericType = typeDict.MakeGenericType(typeArgs);

            //Creating Instance for Dictionary<K,T>.
            IDictionary d = Activator.CreateInstance(genericType) as IDictionary;


            //List<Person> person= new List<Person>();
            //person.Where(p => p.Name == "").AsQueryable();

            //Expression<Action<List<Person>,Action<Person>>> expr= (collection,predicate) =>collection.ForEach(predicate) ;
            //var bodyExpression = expr.Body;
            //var parameters = expr.Parameters;
            ////Assert.AreEqual(bodyExpression.NodeType, ExpressionType.MemberAccess);
            ////Assert.IsNotNull(bodyExpression as MemberExpression);
            ////Assert.AreEqual(expr.ReturnType, typeof(string));
            ////Assert.IsNotNull((bodyExpression as MemberExpression).Member.Name, "Name");
        }



    }
}