using LinqExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqExtensionUnitTest.UnitTestOfExamples
{

    public class ControllerCalculator
    {
        private decimal Vat = 0.23m;

        public decimal Calculate(decimal wealth)
        {
            return wealth*Vat;
        }
    }

    [TestClass]
    public class ActionMethodDispatcherTest
    {
        [TestMethod]
        public void SimpleMethodCall()
        {
          //  var actionMethodDispatcher= new ActionMethodDispatcher<ControllerCalculator>(x=>x.Calculate());
        }
       
    }
}
