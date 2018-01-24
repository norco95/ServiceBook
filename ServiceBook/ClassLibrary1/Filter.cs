using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public static class Filter
    {
        /// <summary>
        /// 	Creates the filter expression
        /// </summary>
        /// <typeparam name = "T">The type of the elements</typeparam>
        /// <param name = "property">The filter column</param>
        /// <param name = "filter">The filter value</param>
        /// <returns>The filter expression</returns>
        public static Expression<Func<T, bool>> From<T>(string property, string filter)
        {
            //Get parameter
            ParameterExpression c = Expression.Parameter(typeof(T), "c");
            //Get property from <T>
            Expression tagsProperty = c;

            if (property.Contains("."))
            {
                // nested property: obj.Prop1.Prop2.PropX
                string[] properties = property.Split(new[] { '.' });
                tagsProperty = properties.Aggregate(tagsProperty, (current, item) => Expression.Property(current, item));
            }
            else
            {
                tagsProperty = Expression.Property(tagsProperty, property);
            }

            MethodInfo myCompareInfo;
            ConstantExpression constant;
            Expression myTagExpression;

            //set the filter to true/false for boolean properties
            if (tagsProperty.Type == typeof(bool) || tagsProperty.Type == typeof(bool?))
            {
                filter = filter == "0" ? "false" : "true";
                myCompareInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                constant = Expression.Constant(filter);
                myTagExpression = Expression.Call(tagsProperty, myCompareInfo, constant);
            }
            else if (tagsProperty.Type == typeof(int))
            {
                myCompareInfo = tagsProperty.Type.GetMethod("Equals", new[] { tagsProperty.Type });
                constant = Expression.Constant(Int32.Parse(filter), tagsProperty.Type);
                myTagExpression = Expression.Call(tagsProperty, myCompareInfo, constant);
            }
            else if (tagsProperty.Type == typeof(int?))
            {
                myCompareInfo = tagsProperty.Type.GetMethod("Equals", new[] { tagsProperty.Type });
                constant = Expression.Constant(Int32.Parse(filter), typeof(Object));
                myTagExpression = Expression.Call(tagsProperty, myCompareInfo, constant);
            }
            else if (tagsProperty.Type == typeof(decimal))
            {
                myCompareInfo = tagsProperty.Type.GetMethod("Equals", new[] { tagsProperty.Type });
                constant = Expression.Constant(Decimal.Parse(filter), tagsProperty.Type);
                myTagExpression = Expression.Call(tagsProperty, myCompareInfo, constant);
            }
            else if (tagsProperty.Type == typeof(decimal?))
            {
                myCompareInfo = tagsProperty.Type.GetMethod("Equals", new[] { tagsProperty.Type });
                constant = Expression.Constant(Decimal.Parse(filter), typeof(Object));
                myTagExpression = Expression.Call(tagsProperty, myCompareInfo, constant);
            }
            else if (tagsProperty.Type == typeof(byte))
            {
                myCompareInfo = tagsProperty.Type.GetMethod("Equals", new[] { tagsProperty.Type });
                constant = Expression.Constant(Byte.Parse(filter), tagsProperty.Type);
                myTagExpression = Expression.Call(tagsProperty, myCompareInfo, constant);
            }
            else if (tagsProperty.Type == typeof(byte?))
            {
                myCompareInfo = tagsProperty.Type.GetMethod("Equals", new[] { tagsProperty.Type });
                constant = Expression.Constant(Byte.Parse(filter), typeof(Object));
                myTagExpression = Expression.Call(tagsProperty, myCompareInfo, constant);
            }
            else // by default, for strings, use the static method for strings
            {
                myCompareInfo = typeof(ExtensionCaseInsensitiveContains).GetMethod("ContainsIgnoreCase", new[] { typeof(string), typeof(string) });
                constant = Expression.Constant(filter.ToLower());
                myTagExpression = Expression.Call(null, myCompareInfo, new[] { tagsProperty, constant });
            }

            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(myTagExpression, c);
            return predicate;
        }
    }

    public static class ExtensionCaseInsensitiveContains
    {
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source.IndexOf(value, comparisonType) != -1;
        }

        public static bool Contains(this string source, string value, bool ignoreCase)
        {
            return ignoreCase ? source.Contains(value, StringComparison.InvariantCultureIgnoreCase) : source.Contains(value, StringComparison.InvariantCulture);
        }

        public static bool ContainsIgnoreCase(this string source, string value)
        {
            return source.Contains(value, true);
        }
    }
}
