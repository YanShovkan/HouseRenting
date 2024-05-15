using System.Linq.Expressions;

namespace HouseRenting.Dal.Specifications.Base;

public class OrSpecification<T> : ComposeSpecification<T>
{
    public OrSpecification(Specification<T> first, Specification<T> second)
        : base(Expression.OrElse, first, second)
    {
    }
}

