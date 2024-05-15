using HouseRenting.Core.Services.Contracts;
using System.Net.Mail;
using System.Net;
using HouseRenting.Dal.Entities;

namespace HouseRenting.Core.Services;
public class EmailSender : IEmailSender
{
    public void SendEmailAdoutMeet(DateTime date, Advert advert, Admin admin, bool isNew, DateTime? oldDate = default)
    {
        using (var client = new SmtpClient())
        {
            client.Host = "smtp.mail.ru";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("rent.sender@mail.ru", "ma4wkUCJUz9E0NVt2KEF");
            using (var message = new MailMessage(
                from: new MailAddress("rent.sender@mail.ru", "Риэлторское агентство"),
                to: new MailAddress(advert.ClientEmail)
                ))
            {
                if (isNew)
                {
                    message.Subject = "Вам назначена встреча на просмотр квартиры!";
                    message.Body = $"Назначена встреча на просмотр квартиры {date.ToShortDateString()} в {date.ToShortTimeString()}. Интересует квартира по адресу {advert.Address}. Риэлтором, ведущим сделку является {admin.Name}. При наличии вопросов или переносе времени встречи обращайтесь на почту {admin.Email}.";
                }
                else
                {
                    message.Subject = "Изменение времени встречи на просмотр квартиры!";
                    message.Body = $"Назначенная встреча на просмотр квартиры {oldDate?.ToShortDateString()} в {oldDate?.ToShortTimeString()} перенесена на {date.ToShortDateString()} в {date.ToShortTimeString()}. Интересует квартира по адресу {advert.Address}. Риэлтором, ведущим сделку является {admin.Name}. При наличии вопросов или переносе времени встречи обращайтесь на почту {admin.Email}.";
                }

                client.Send(message);
            }
        }
    }
}
