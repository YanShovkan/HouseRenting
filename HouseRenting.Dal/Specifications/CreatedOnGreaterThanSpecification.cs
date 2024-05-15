using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Specifications.Base;

namespace HouseRenting.Dal.Specifications;
public class CreatedOnGreaterThanSpecification : ExpressionSpecification<Advert>
{
    public CreatedOnGreaterThanSpecification(DateTime createdOn) : base(c => c.CreatedOn >= createdOn)
    {
    }
}
