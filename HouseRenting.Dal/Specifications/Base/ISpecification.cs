namespace HouseRenting.Dal.Specifications.Base;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T entity);
}
