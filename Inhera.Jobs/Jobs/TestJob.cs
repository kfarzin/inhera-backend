using Quartz;

namespace Inhera.Jobs.Jobs
{
    [DisallowConcurrentExecution]
    public class TestJob : IJob
    {
        //private readonly GenericDeliverableMessageService genericDeliverableMessageService;
        //public TestJob(GenericDeliverableMessageService genericDeliverableMessageService)
        //{
        //    this.genericDeliverableMessageService = genericDeliverableMessageService;
        //}
        //public async Task Execute(IJobExecutionContext context)
        //{
        //    var deliverableMessage = genericDeliverableMessageService.GetDefault(Shared.Models.Enums.MessageDeliveryTypes.Email);
        //    deliverableMessage.Audiences = GenericDeliverableMessage.ToJsonbString("farzin@daensk.de");
        //    deliverableMessage.Data = GenericDeliverableMessage.ToJsonbString(new
        //    {
        //        EmailTo = "some email and money",
        //    });
        //    //todo: translate
        //    deliverableMessage.Subject = "welcome to the club";
        //    deliverableMessage.Template = "templates/User/WelcomeMessage";
        //    deliverableMessage.ModelType = GenericDeliverableMessageModelTypes.TestType;
        //    var result = await genericDeliverableMessageService.Add(deliverableMessage);
        //}
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
