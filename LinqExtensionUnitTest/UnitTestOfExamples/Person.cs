using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{
    public class Person
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime JoiningDate { get; set; }

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;

        }

        public Person()
        {
            
        }
    }
}
