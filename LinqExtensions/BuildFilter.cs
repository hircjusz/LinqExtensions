using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExtensions
{public enum FilterOperation
		{
			Equal,
			DoesNotEqual,
			LessThan,
			LessThanOrEqual,
			GreaterThan,
			GreaterThanOrEqual,
			Contains
		};
    public static class FilterBuilder
    {
        /*
         public static class FilterBuilder
	{
		
		public static Filter

  BuildFilter
 
  (string fieldName, object expression, Type expressionType, FilterOperation operation)
		{
			ParameterExpression selectedRow = Expression.Parameter(typeof(O), "selectedRow");
			MemberExpression field = Expression.Property(selectedRow, fieldName);
			var converter = TypeDescriptor.GetConverter(expressionType);
			var result = converter.ConvertFrom(expression);
			ConstantExpression expr = Expression.Constant(result, expressionType);
			Expression notNull = Expression.NotEqual(field, Expression.Constant(null, field.Type));
			Expression op; 
			switch (operation)
			{
				case FilterOperation.LessThan:
					op = Expression.AndAlso(notNull, Expression.LessThan(field, expr));	
					break;
				case FilterOperation.LessThanOrEqual:
					op = Expression.AndAlso(notNull, Expression.LessThanOrEqual(field, expr));
					break;
				case FilterOperation.GreaterThan:
					op = Expression.AndAlso(notNull, Expression.GreaterThan(field, expr));
					break;
				case FilterOperation.GreaterThanOrEqual:
					op = Expression.AndAlso(notNull, Expression.GreaterThanOrEqual(field, expr));
					break;
				case FilterOperation.Contains:
					Type stringType = typeof(string);
					MethodInfo method = stringType.GetMethod("Contains", new[] { stringType });
					op = Expression.AndAlso( notNull, Expression.Call(field, method, expr ));
					break;
				case FilterOperation.DoesNotEqual:
					op = Expression.NotEqual(field, expr);
					break;
				default:
					op = Expression.Equal(field, expr);
					break;
			}
			var fe = Expression.Lambda
  
   
	> (op, new ParameterExpression[] { selectedRow }).Compile();
			return new Filter
	
	 (fe, operation, fieldName, expression);
		}
		public class Filter
	 
		{
			public Func
	  
		FilterExpression {get; set;}
			public FilterOperation Operation { get; set; }
			public string Fieldname { get; set; }
			public object Expression { get; set; }
			public Filter(Func
	   
		 fe, FilterOperation fo, string fn, object ex)
			{
				this.FilterExpression = fe;
				this.Operation = fo;
				this.Fieldname = fn;
				this.Expression = ex;
			}
			public override string ToString()
			{
				return string.Format("'{0}' {1} '{2}'", Fieldname, Operation, Expression);
			}
		}
	}
	   

         */


    }
}
