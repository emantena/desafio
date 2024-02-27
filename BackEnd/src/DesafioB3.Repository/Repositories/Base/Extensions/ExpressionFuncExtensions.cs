using System.Linq.Expressions;
using System.Reflection;

namespace DesafioB3.Repository.Repositories.Base.Extensions;
internal static class ExpressionFuncExtensions
{
    public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> expression)
    {
        MemberExpression memberExpression = null;

        switch (expression.Body.NodeType)
        {
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
                memberExpression = (expression.Body as UnaryExpression)?.Operand as MemberExpression ?? null;
                break;
            default:
                memberExpression = expression.Body as MemberExpression;
                break;
        }

        if (memberExpression == null)
        {
            throw new ArgumentException("The expression not refer to a property.");
        }

        var propertyInfo = memberExpression.Member as PropertyInfo;

        if (propertyInfo == null)
        {
            throw new ArgumentException("The expression not refer to a property.");
        }

        return propertyInfo;
    }
}