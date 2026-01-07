var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Inhera_CoreAPI>("Inhera-coreapi");

builder.AddProject<Projects.Inhera_NotificationService>("Inhera-notificationservice");

builder.AddProject<Projects.Inhera_Jobs>("Inhera-jobs");

builder.AddProject<Projects.Inhera_AdminAPI>("Inhera-adminapi");

builder.AddProject<Projects.Inhera_Infrastructure>("Inhera-infrastructure");

builder.Build().Run();
