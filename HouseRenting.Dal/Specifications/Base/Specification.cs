using System.Linq.Expressions;

namespace HouseRenting.Dal.Specifications.Base;

public abstract class Specification<T> : ISpecification<T>
{
    private readonly Lazy<Func<T, bool>> _compiledExpression;

    protected Func<T, bool> CompiledExpression => _compiledExpression.Value;

    public Expression<Func<T, bool>> Predicate => ToExpression();

    protected Specification()
    {
        _compiledExpression = new Lazy<Func<T, bool>>(() => Predicate.Compile());
    }

    protected abstract Expression<Func<T, bool>> ToExpression();

    public static Specification<T> operator !(Specification<T> spec)
    {
        return new NotSpecification<T>(spec);
    }

    public static Specification<T> operator &(Specification<T> first, Specification<T> second)
    {
        return new AndSpecification<T>(first, second);
    }

    public static Specification<T> operator |(Specification<T> first, Specification<T> second)
    {
        return new OrSpecification<T>(first, second);
    }

    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification.Predicate;
    }

    public static implicit operator Func<T, bool>(Specification<T> specification)
    {
        return specification.CompiledExpression;
    }

    public bool IsSatisfiedBy(T entity)
    {
        return CompiledExpression(entity);
    }
}