using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Specifications.Base;

namespace HouseRenting.Dal.Specifications;
public class PriceLessThenSpecification : ExpressionSpecification<Advert>
{
    public PriceLessThenSpecification(decimal price) : base(c => c.Price < price)
    {
    }
}
