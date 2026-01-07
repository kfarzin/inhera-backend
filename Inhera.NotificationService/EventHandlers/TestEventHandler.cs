using DotNetCore.CAP;
using Inhera.NotificationService.Data;
using Inhera.NotificationService.Models.Entities.SQL;
using Inhera.NotificationService.Models.Enums;
using Inhera.NotificationService.Models.Vms.Authentication;
using Inhera.NotificationService.Services;
using Inhera.Shared.Models.DomainEvents;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using static Inhera.NotificationService.Services.ViewRendererService;

namespace Inhera.NotificationService.EventHandlers
{
    public class TestEventHandler : ICapSubscribe
    {
        private readonly MainContext mainContext;
        private readonly IViewRenderService viewRendererService;
        private readonly EmailSenderService emailSenderService;
        public TestEventHandler(MainContext mainContext, 
            IViewRenderService viewRendererService,
            EmailSenderService emailSenderService)
        {
            this.mainContext = mainContext;
            this.viewRendererService = viewRendererService;
            this.emailSenderService = emailSenderService;
        }

        // Group = consumer group (competing consumers across replicas)
        [CapSubscribe(ChannelNames.Generic, Group = "notification-service")]
        //public Task Handle(OrderSubmitted msg)
        public async Task Handle(AccountRegistrationDomainEvent msg)
        {
            try
            {
                // Add idempotency check to prevent duplicate processing
                var existingEntry = await mainContext.Counters
                    .FirstOrDefaultAsync(c => c.Title == $"Order-{msg.Code}");

                if (existingEntry != null)
                {
                    // Already processed, skip
                    return;
                }

                var counterentry = new CounterEntity
                {
                    Id = Guid.NewGuid(),
                    Title = $"Order-{msg.Code}", // Make it unique to the order
                };

                mainContext.Counters.Add(counterentry);

                //

                GenericDeliverableMessage deliverableMessage = new()
                {
                    MessageDeliveryType = MessageDeliveryTypes.Email,
                };                
                var model = new AuthLoginCodeVm
                {
                    Email = "some email and money",
                    Code = "some code",
                };
                deliverableMessage.Audiences = GenericDeliverableMessage.ToJsonbString("farzin_fz@yahoo.com");
                deliverableMessage.Data = GenericDeliverableMessage.ToJsonbString(model);
                //todo: translate
                deliverableMessage.Subject = "welcome to the club";
                deliverableMessage.Template = "templates/Authentication/en/AuthLoginCode";
                deliverableMessage.ModelType = GenericDeliverableMessageModelTypes.TestType;
                var result = mainContext.genericDeliverableMessages.Add(deliverableMessage);
                //await mainContext.SaveChangesAsync();

                // Send email
                await emailSenderService.Send<AuthLoginCodeVm>(deliverableMessage, model);
                //await SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error processing order {msg.Code}: {ex.Message}");
                throw; // Re-throw to let CAP handle retry logic
            }
        }
    }
}

public record OrderSubmitted(
    Guid OrderId,
    string CustomerId,
    decimal Total,
    DateTime SubmittedAtUtc
);
