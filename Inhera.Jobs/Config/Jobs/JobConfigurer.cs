using Quartz;

namespace Inhera.Jobs.Config.Jobs
{
    public static class JobConfigurer
    {
        public async static void Configure(IScheduler scheduler)
        {            
        }
    }

    public static class JobNames
    {
        public const string INHERA_INSPECTION_JOB = "INHERA_INSPECTION_JOB";
        public const string INHERA_COLLECTION_JOB = "INHERA_COLLECTION_JOB";

    }

    public static class TriggerNames
    {
        public const string INHERA_INSPECTION_TRIGGER = "INHERA_INSPECTION_TRIGGER";
        public const string INHERA_COLLECTION_TRIGGER = "INHERA_COLLECTION_TRIGGER";

    }

    public static class JobGroups
    {
        public const string INHERA = "INHERA";
        
    }

    public static class TriggerGroups
    {
        public const string INHERA = "INHERA";

    }
}
