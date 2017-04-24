using System;
using System.Collections;
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
        private readonly LambdaExpressionTest _lambdaExpressionTest = new LambdaExpressionTest();

        [TestMethod]
        public void CallUpper()
        {
            var sampleString = "sample string";
            Expression callExpr = Expression.Call(Expression.Constant(sampleString),
                typeof(String).GetMethod("ToUpper", new Type[] { }));
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
            var containsMethodExp = Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(string) }, someValue,
                parameterExp);
            var lamda = Expression.Lambda<Func<string, bool>>(containsMethodExp, parameterExp);

            //ACT
            var flag = lamda.Compile()("PETR");
            Assert.IsTrue(flag);
            var flag2 = lamda.Compile()("PETR2");
            Assert.IsFalse(flag2);
        }


        [Ignore]
        public void CallContainsFilteredList()
        {
            //ARRANGE
            var target = new List<Person>() { new Person() { Name = "PETR" }, new Person() { Name = "VALE" } };

            var searchValues = new List<Person>() { new Person() { Name = "PETR" } };
            var targetType = typeof(Person);
            var containsLambdaParameter = Expression.Parameter(targetType, "p");
            var property = Expression.Property(containsLambdaParameter, targetType, "Name");
            var searchValuesAsConstant = Expression.Constant(searchValues, searchValues.GetType());

            var containsBody = Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(string) }, searchValuesAsConstant, property);
            //Create a lambda expression with the parameter p -> p => searchValues.Contains(p.Id)
            var containsLambda = Expression.Lambda(containsBody, containsLambdaParameter);

            //Create a constant with the -> IEnumerable<T> target
            var targetAsConstant = Expression.Constant(target, target.GetType());

            //Where(p => searchValues.Contains(p.Id))
            var whereBody = Expression.Call(typeof(List<Person>), "Where", new[] { targetType }, targetAsConstant, containsLambda);

            //target.Where(p => searchValues.Contains(p.Id))
            var whereLambda = Expression.Lambda<Func<List<Person>>>(whereBody).Compile();

            var result= whereLambda.Invoke();

            //ACT
            //var flag = lamda.Compile()("PETR");
            //Assert.IsTrue(flag);
            //var flag2 = lamda.Compile()("VALE");
            //Assert.IsTrue(flag2);
            //var flag3 = lamda.Compile()("PETR2");
            //Assert.IsFalse(flag3);
        }

        

        [TestMethod]
        public void MethodCallPowWithConstant()
        {
            Expression callExpr = Expression.Call(
                typeof(Math).GetMethod("Pow", new[] { typeof(double), typeof(double) }),
                Expression.Constant((double)2),
                Expression.Constant((double)2));

            var result = Expression.Lambda<Func<double>>(callExpr).Compile()();
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void MethodCallPowWithParameter()
        {
            var param1 = Expression.Parameter(typeof(double), "p");
            var param2 = Expression.Parameter(typeof(double), "s");

            Expression callExpr = Expression.Call(
                typeof(Math).GetMethod("Pow", new[] { typeof(double), typeof(double) }),
                param1,
                param2);

            var result = Expression.Lambda<Func<double, double, double>>(callExpr, param1, param2).Compile()(2, 2);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void MethodCallSinWithParameter()
        {
            var param1 = Expression.Parameter(typeof(double), "p");
            var param2 = Expression.Parameter(typeof(double), "s");

            Expression powExpr = Expression.Call(
                typeof(Math).GetMethod("Pow", new[] { typeof(double), typeof(double) }),
                param1,
                param2);


            Expression sinExpr = Expression.Call(
                typeof(Math).GetMethod("Sin", new[] { typeof(double) }), powExpr);


            var result = Expression.Lambda<Func<double, double, double>>(sinExpr, param1, param2).Compile()(2, 2);
            Assert.AreEqual(Math.Sin(Math.Pow(2, 2)), result);
        }

        [TestMethod]
        public void MethodCallIndexArrayAccess()
        {
            string[,] gradeArray =
    { { "chemistry", "history", "mathematics"},{ "chemistry", "history", "mathematics"}};

            System.Linq.Expressions.Expression arrayExpression =
    System.Linq.Expressions.Expression.Constant(gradeArray);

            var index0 = 0;
            var index1 = 2;

            MethodCallExpression methodCallExpression =
    Expression.ArrayIndex(
         arrayExpression,
        Expression.Constant(index0),
         Expression.Constant(index1));
            //Expression.ArrayIndex()

            var result = Expression.Lambda<Func<string>>(methodCallExpression).Compile()();
            Assert.AreEqual(gradeArray[index0, index1], result);

        }

        [TestMethod]
        public void MethodCallStartsWith()
        {
           //var t= "Dish xxx".StartsWith("Dish");

            var parameter = Expression.Parameter(typeof (string), "name");
            var argument = Expression.Constant("Dish");
            var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

            var methodCall = Expression.Call(parameter,method, argument);

            var lambda = Expression.Lambda<Func<string,bool>>(methodCall, parameter);
            var result=lambda.Compile()("Dish xxxxxx");

            Assert.AreEqual(true,result);
        }
    }
}
