using System;
using System.Collections;
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

            var param = Expression.Parameter(typeof(Person), "Person");
            var ex = Expression.Property(param, "Email");
            var sort = Expression.Lambda<Func<Person, object>>(ex, param);
            var sortedData = (from s in people select s).OrderBy(sort.Compile()).ToList();
        }

        [TestMethod]
        public void StartsWithExpression()
        {
            var param = Expression.Parameter(typeof(Person), "person");
            var ex = Expression.Property(param, "Email");

            var mi = typeof(String).GetMethod("StartsWith", new Type[] { typeof(string) });
            var startWithExpr = Expression.Call(ex, mi, Expression.Constant("H"));
            var lambdaExpr = Expression.Lambda<Func<Person, bool>>(startWithExpr, param);

            List<Person> people = new List<Person>
            {
                new Person(){ Name = "Pranay",Email="pranay@test.com" },
                new Person(){ Name = "Heamng",Email="Hemang@test.com" },
                new Person(){ Name = "Hiral" ,Email="Hiral@test.com"},
                new Person(){ Name = "Maitri",Email="Maitri@test.com" }
            };

            var searchedData = people.Where(lambdaExpr.Compile()).ToList();
            Assert.AreEqual(2, searchedData.Count);
        }

        [TestMethod]
        public void ComplexDivideSectionMethod()
        {

            //Example
            Func<IEnumerable<int>, int, bool> dividesectionmethod = (x, y) =>
            {
                int nos1 = 0;
                int nos2 = 0;
                foreach (int i in x)
                {
                    if (i <= y)
                        nos1++;
                    else
                        nos2++;
                }
                return nos1 > nos2;
            };


            var enumerableExpression = Expression.Parameter(typeof (IEnumerable<int>), "x");
            var inEExpression = Expression.Parameter(typeof (int), "y");

            var localvarnos1 = Expression.Variable(typeof (int), "nos1");
            var localvarnos2 = Expression.Variable(typeof (int), "nos2");

            var zeroCondidtional = Expression.Constant(0);

            var bexplocalnos1 = Expression.Assign(localvarnos1, zeroCondidtional);
            var bexplocalnos2 = Expression.Assign(localvarnos2, zeroCondidtional);

            var enumerator = Expression.Variable(typeof (IEnumerator<int>), "enumerator");

            var assignEnumerator = Expression.Assign(enumerator,
                Expression.Call(enumerableExpression, typeof (IEnumerable<int>).GetMethod("GetEnumerator")));

            var moveNext = Expression.Call(enumerator, typeof (IEnumerator).GetMethod("MoveNext"));


        }

        [TestMethod]
        public void CreateFieldExpression()
        {

            var horse=new Models.Animal() {Species = "Horse"};
            var memberExpressiion =  Expression.Field(Expression.Constant(horse), "Species");
            Assert.AreEqual("Horse",memberExpressiion.Member.Name);
            

        }
    }
}
