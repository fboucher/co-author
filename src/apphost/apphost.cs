#:sdk Aspire.AppHost.Sdk@13.1.0

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.webapp>("webapp");

builder.Build().Run();
