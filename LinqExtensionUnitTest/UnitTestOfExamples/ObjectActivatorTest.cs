using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LinqExtensions;
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

        [TestMethod]
        public void NewExpressionToCreateTypeConstAsParameter()
        {
            var personTest = new Person {Name = "abc", Age = 123};
            Type anonType = personTest.GetType();

            var exp = Expression.New(
            anonType.GetConstructor(new[] { typeof(string), typeof(int) }),
            Expression.Constant("abc"),
            Expression.Constant(123));
            var lambda = LambdaExpression.Lambda(exp);
            Person myObj = lambda.Compile().DynamicInvoke() as Person;
            Assert.IsNotNull(myObj);
            Assert.AreEqual(personTest.Name,myObj.Name);
            Assert.AreEqual(personTest.Age,myObj.Age);
        }

        [TestMethod]
        public void NewExpressionToCreateConfigurableParameter()
        {
            //ARRANGE
            string name = "abc";
            int age = 123;

            var personTest = new Person { Name = name, Age = age };
            Type anonType = personTest.GetType();

            var parameterName = Expression.Parameter(typeof (string), "name");
            var parameterAge = Expression.Parameter(typeof (int), "age");

            var exp = Expression.New(
            anonType.GetConstructor(new[] { typeof(string), typeof(int) }),
            parameterName,
            parameterAge);
            var lambda = LambdaExpression.Lambda(exp,parameterName,parameterAge);
            Person myObj = lambda.Compile().DynamicInvoke(name,age) as Person;
            Assert.IsNotNull(myObj);
            Assert.AreEqual(personTest.Name, myObj.Name);
            Assert.AreEqual(personTest.Age, myObj.Age);
        }

        [TestMethod]
        public void ObjectActivatorTest1()
        {
            var personActivator=  ActivatorUtil.GetActivator<Person>("name",123);
            var person = personActivator("name", 123);
            Assert.AreEqual(person.Name,"name");
            Assert.AreEqual(person.Age,123);

        }

        [TestMethod]
        public void ObjectGetterAndSetter()
        {
            Func<bool> Getter;
            Action<bool> Setter;
            PropertyInfo prop = null;
            Getter = Expression.Lambda<Func<bool>>(Expression.Property(null, prop)).Compile();
            ParameterExpression value = Expression.Parameter(typeof(bool));
            Setter = Expression.Lambda<Action<bool>>(
                Expression.Assign(Expression.Property(null, prop), value), value).Compile();


        }
    }
}
