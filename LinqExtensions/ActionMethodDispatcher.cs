using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqExtensions
{
    public class ActionMethodDispatcher<T>
    {

        private ActionExecutor _executor;
        public MethodInfo MethodInfo { get; private set; }

        private delegate object ActionExecutor(object obj, object[] parameters);

        private delegate void VoidActionExecutor(object obj, object[] parameters);

        //public ActionMethodDispatcher(MethodInfo methodInfo)
        //{
        //    this._executor = GetExecutor(methodInfo);
        //    this.MethodInfo = methodInfo;
        //}

        public ActionMethodDispatcher(Expression expr )
        {
            //this._executor = GetExecutor(methodInfo);
            //this.MethodInfo = methodInfo;
        }


        public object Execute(object obj, object[] parameters)
        {
            return _executor(obj, parameters);
        }


        private static ActionExecutor GetExecutor(MethodInfo methodInfo)
        {

            return (o, parameters) => 1;
        }



    }
}
