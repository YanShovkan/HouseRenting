using System.Linq.Expressions;

namespace HouseRenting.Dal.Specifications.Base;

internal class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _spec;

    public NotSpecification(Specification<T> spec)
    {
        _spec = spec;
    }

    protected override Expression<Func<T, bool>> ToExpression()
    {
        var negateExpression = (Expression)Expression.Not(_spec.Predicate.Body);

        return Expression.Lambda<Func<T, bool>>(
            negateExpression,
            _spec.Predicate.Parameters[0]);
    }
}

