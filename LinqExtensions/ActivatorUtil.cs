using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqExtensions
{
    public static class ActivatorUtil
    {
        public delegate T ObjectActivator<T>(params object[] args);

        public static ObjectActivator<T> GetActivator<T>(params object[] args)
        {
            var ctors = typeof(T).GetConstructors();
            var ctor =
                ctors.FirstOrDefault(t => t.GetParameters().Count() == args.Length);

            ParameterInfo[] paramsInfo = ctor.GetParameters();

            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");
            Expression[] argsExp = new Expression[paramsInfo.Length];

            for (int i = 0; i < paramsInfo.Length; i++)
            {
                var index = Expression.Constant(i);
                var paramType = paramsInfo[i].ParameterType;
                var paramAccessor = Expression.ArrayIndex(param, index);
                var paramCastExpr = Expression.Convert(paramAccessor, paramType);
                argsExp[i] = paramCastExpr;
            }
            var newExpr = Expression.New(ctor, argsExp);
            var lambda = Expression.Lambda(typeof (ObjectActivator<T>), newExpr, param);
            var compiled = lambda.Compile() as ObjectActivator<T>;
            return compiled;
        }


    }
}
