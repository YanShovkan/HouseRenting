using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Specifications.Base;

namespace HouseRenting.Dal.Specifications;

public class AreaLessThenSpecification : ExpressionSpecification<Advert>
{
    public AreaLessThenSpecification(decimal area) : base(c => c.Area < area)
    {
    }
}
