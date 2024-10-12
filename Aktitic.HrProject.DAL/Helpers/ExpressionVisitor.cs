using System.Linq.Expressions;

namespace Aktitic.HrProject.DAL.Helpers;

public class ExpressionVisitor(Expression oldValue, Expression newValue) : System.Linq.Expressions.ExpressionVisitor
{
    public static Expression ReplaceParameter(Expression expression, Expression oldValue, Expression newValue)
    {
        return new ExpressionVisitor(oldValue, newValue).Visit(expression);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == oldValue ? newValue : node;
    }
}