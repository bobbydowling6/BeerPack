using Microsoft.AspNet.Identity;
using System;
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
            //Set this to a template ID generated from your SendGrid transactional Email Templates
            mail.SetTemplateId("356743ee-55be-4531-9be3-d4b116b50c16");

            mail.AddSubstitution("<%copyright%>", string.Format("©{0} BeerPack", DateTime.Now.Year.ToString()));

            return client.SendEmailAsync(mail);
        }
    }
}