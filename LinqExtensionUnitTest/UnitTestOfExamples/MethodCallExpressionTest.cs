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


        //[TestMethod]
        //public void CallContainsFilteredList()
        //{
        //    //ARRANGE
        //    var values = new List<Person>() { new Person() { Name = "PETR" }, new Person() { Name = "VALE" } };
        //    var type = typeof(Person);
        //    var parameterExp = Expression.Parameter(type, "");
        //    var propertyExpression = Expression.Property(parameterExp, "Name");
        //    var someValue = Expression.Constant(values, typeof(IEnumerable<Person>));
        //    var containsMethodExp = Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(string) }, someValue, propertyExpression);
        //    var lamda = Expression.Lambda<Func<Person, bool>>(containsMethodExp, parameterExp);

        //    //ACT
        //    var flag = lamda.Compile()("PETR");
        //    Assert.IsTrue(flag);
        //    var flag2 = lamda.Compile()("VALE");
        //    Assert.IsTrue(flag2);
        //    var flag3 = lamda.Compile()("PETR2");
        //    Assert.IsFalse(flag3);
        //}

        /*
         static IEnumerable GetFilteredList(IEnumerable target, string propertyName, IEnumerable searchValues)
{
    //Get target's T 
    var targetType = target.GetType().GetGenericArguments().FirstOrDefault();
    if (targetType == null)
        throw new ArgumentException("Should be IEnumerable<T>", "target");

    //Get searchValues's T
    var searchValuesType = searchValues.GetType().GetGenericArguments().FirstOrDefault();
    if (searchValuesType == null)
        throw new ArgumentException("Should be IEnumerable<T>", "searchValues");

    //Create a p parameter with the type T of the items in the -> target IEnumerable<T>
    var containsLambdaParameter = Expression.Parameter(targetType, "p");

    //Create a property accessor using the property name -> p.#propertyName#
    var property = Expression.Property(containsLambdaParameter, targetType, propertyName);

    //Create a constant with the -> IEnumerable<T> searchValues
    var searchValuesAsConstant = Expression.Constant(searchValues, searchValues.GetType());

    //Create a method call -> searchValues.Contains(p.Id)
    var containsBody = Expression.Call(typeof(Enumerable), "Contains", new[] { searchValuesType }, searchValuesAsConstant, property);

    //Create a lambda expression with the parameter p -> p => searchValues.Contains(p.Id)
    var containsLambda = Expression.Lambda(containsBody, containsLambdaParameter);

    //Create a constant with the -> IEnumerable<T> target
    var targetAsConstant = Expression.Constant(target, target.GetType());

    //Where(p => searchValues.Contains(p.Id))
    var whereBody = Expression.Call(typeof(Enumerable), "Where", new[] { targetType }, targetAsConstant, containsLambda);

    //target.Where(p => searchValues.Contains(p.Id))
    var whereLambda = Expression.Lambda<Func<IEnumerable>>(whereBody).Compile();

    return whereLambda.Invoke();
}
         */

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

    }
}
