using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var apiKey = builder.Configuration["AppSettings:REKA_API_KEY"] ?? Environment.GetEnvironmentVariable("REKA_API_KEY") ?? throw new InvalidOperationException("REKA_API_KEY environment variable is not set.");

var compose = builder.AddDockerComposeEnvironment("docker-env");


builder.AddProject<Projects.webapp>("webApp")
    .WithExternalHttpEndpoints()
    .WithEnvironment("REKA_API_KEY", apiKey)
    .WithComputeEnvironment(compose);;

builder.Build().Run();
