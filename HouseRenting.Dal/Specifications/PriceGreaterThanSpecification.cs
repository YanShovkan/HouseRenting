using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Specifications.Base;

namespace HouseRenting.Dal.Specifications;

public class PriceGreaterThanSpecification : ExpressionSpecification<Advert>
{
    public PriceGreaterThanSpecification(decimal price) : base(c => c.Price >= price)
    {
    }
}
