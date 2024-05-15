using System.Linq.Expressions;

namespace HouseRenting.Dal.Specifications.Base;

public class ComposeSpecification<T> : Specification<T>
{
    private readonly Func<Expression, Expression, Expression> _compose;
    private readonly Specification<T> _first;
    private readonly Specification<T> _second;

    protected ComposeSpecification(
        Func<Expression, Expression, Expression> compose,
        Specification<T> first,
        Specification<T> second)
    {
        _compose = compose;
        _first = first;
        _second = second;
    }

    protected override Expression<Func<T, bool>> ToExpression()
    {
        var parameter = Expression.Parameter(typeof(T));

        var replacedFirst = ReplaceParam(_first.Predicate, parameter);
        var replacedSecond = ReplaceParam(_second.Predicate, parameter);

        return Expression.Lambda<Func<T, bool>>(
            _compose(replacedFirst, replacedSecond),
            parameter);
    }

    private static Expression ReplaceParam(Expression<Func<T, bool>> expression, Expression parameter)
    {
        return new ReplaceExpressionVisitor(expression.Parameters[0], parameter)
            .Visit(expression.Body)!;
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression? Visit(Expression? node)
        {
            return node == _oldValue ? _newValue : base.Visit(node);
        }
    }
}
