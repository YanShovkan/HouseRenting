using HouseRenting.Dal.Entities;

namespace HouseRenting.Core.Services.Contracts;

public interface IEmailSender
{
    void SendEmailAdoutMeet(DateTime date, Advert advert, Admin admin, bool isNew, DateTime? oldDate = default);
}
