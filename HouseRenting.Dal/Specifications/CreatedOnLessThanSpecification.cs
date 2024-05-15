using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Specifications.Base;

namespace HouseRenting.Dal.Specifications;

public class CreatedOnLessThanSpecification : ExpressionSpecification<Advert>
{
    public CreatedOnLessThanSpecification(DateTime createdOn) : base(c => c.CreatedOn < createdOn)
    {
    }
}
