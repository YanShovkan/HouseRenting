using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Specifications.Base;

namespace HouseRenting.Dal.Specifications;

public class AreaGreaterThanSpecification : ExpressionSpecification<Advert>
{
    public AreaGreaterThanSpecification(decimal area) : base(c => c.Area >= area)
    {
    }
}
