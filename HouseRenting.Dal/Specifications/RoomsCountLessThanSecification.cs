using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Specifications.Base;

namespace HouseRenting.Dal.Specifications;

public class RoomsCountLessThanSecification : ExpressionSpecification<Advert>
{
    public RoomsCountLessThanSecification(int roomsCount) : base(c => c.RoomsCount < roomsCount)
    {
    }
}