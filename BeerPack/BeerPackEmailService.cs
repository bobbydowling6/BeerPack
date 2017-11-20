using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace BeerPack
{
    internal class BeerPackEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.Key"];
            SendGrid.SendGridClient client = new SendGrid.SendGridClient(apiKey);

            SendGrid.Helpers.Mail.SendGridMessage mail = new SendGrid.Helpers.Mail.SendGridMessage();
            mail.SetFrom(new SendGrid.Helpers.Mail.EmailAddress { Name = "BeerPack Admin", Email = "team@beerpack.com" });
            mail.AddTo(message.Destination);
            mail.SetSubject(message.Subject);
            mail.AddContent("text/plain", message.Body);
            mail.AddContent("text/html", message.Body);

            return client.SendEmailAsync(mail);
        }
    }
}