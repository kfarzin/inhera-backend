using Inhera.NotificationService.Models.Entities.SQL;
using Inhera.Shared.Util.Extensions;
using MailKit.Net.Smtp;
using MimeKit;
using static Inhera.NotificationService.Services.ViewRendererService;

namespace Inhera.NotificationService.Services
{
    public class EmailSenderService
    {
        private readonly IViewRenderService viewRendererService;
        public EmailSenderService(IViewRenderService viewRendererService)
        {
            this.viewRendererService = viewRendererService;
        }

        public async Task Send<T>(GenericDeliverableMessage deliverableMessage, T model)
        {
            var emails = deliverableMessage.Audiences.ToJsonStringArray();

            var data = ConstructModel(deliverableMessage.Data);
            var template = deliverableMessage.Template;

            var message = new MimeMessage();
            message.Subject = deliverableMessage.Subject;
            

            var content = await viewRendererService.RenderToStringAsync(template!, model);
            message.Body = new TextPart("html")
            {
                Text = content,
            };
            message.From.Add(new MailboxAddress("dev mailer", "inhera_web_sender@p2run.org"));
            foreach (var email in emails)
            {
                message.To.Add(new MailboxAddress("Mr development", email));
            }

            var smtpAddress = "smtp.ionos.co.uk";
            var smtpPort = 587;
            var username = "inhera_web_sender@p2run.org";
            var password = "FHGkbtqEyzPT46745674";


            using (var client = new SmtpClient())
            {
                //TODO: to read from config
                client.Connect(smtpAddress, smtpPort, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(username, password);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public async Task Send(MimeMessage message)
        {
            var smtpAddress = "smtp.ionos.co.uk";
            var smtpPort = 587;
            var username = "bibit_web_sender@p2run.org";
            var password = "NVsjXkeJityFHGkbtqEyzPTk";

            using (var client = new SmtpClient())
            {
                //TODO: to read from config
                client.Connect(smtpAddress, smtpPort, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(username, password);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        private IDictionary<string, object> ConstructModel(string? data)
        {
            return data.ToJsonObjectArray();
        }

        public string GetEmailSender(string? template) => template switch
        {
            "templates/User/WelcomeMessage" => "jdev-mailer@daensk.de",
            _ => "jdev-mailer@daensk.de",
        };
    }
}