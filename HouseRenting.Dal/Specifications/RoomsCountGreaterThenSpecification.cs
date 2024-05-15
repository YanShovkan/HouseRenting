using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Specifications.Base;

namespace HouseRenting.Dal.Specifications;
public class RoomsCountGreaterThenSpecification : ExpressionSpecification<Advert>
{
    public RoomsCountGreaterThenSpecification(int roomsCount) : base(c => c.RoomsCount >= roomsCount)
    {
    }
}
