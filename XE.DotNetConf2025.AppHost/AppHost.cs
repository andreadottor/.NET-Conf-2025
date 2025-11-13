var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sql = builder.AddSqlServer("sql")
                 .WithDataVolume()
                 .WithLifetime(ContainerLifetime.Persistent);

var identitydb = sql.AddDatabase("identitydb");

var apiService = builder.AddProject<Projects.XE_DotNetConf2025_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.XE_DotNetConf2025_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(identitydb)
    .WaitFor(identitydb)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
