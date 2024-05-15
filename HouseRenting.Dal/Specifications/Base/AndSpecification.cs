using System.Linq.Expressions;

namespace HouseRenting.Dal.Specifications.Base;
public class AndSpecification<T> : ComposeSpecification<T>
{
    public AndSpecification(Specification<T> first, Specification<T> second)
        : base(Expression.AndAlso, first, second)
    {
    }
}

