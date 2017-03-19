using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void OrderPeople()
        {

            List<Person> people = new List<Person>
            {
                new Person(){ Name = "Pranay",Email="pranay@test.com" },
                new Person(){ Name = "Heamng",Email="Hemang@test.com" },
                new Person(){ Name = "Hiral" ,Email="Hiral@test.com"},
                new Person(){ Name = "Maitri",Email="Maitri@test.com" }
            };

            var param = Expression.Parameter(typeof (Person), "Person");
            var ex = Expression.Property(param, "Email");
            var sort = Expression.Lambda<Func<Person, object>>(ex, param);
            var sortedData = (from s in people select s).OrderBy(sort.Compile()).ToList();
        }
    }
}
