using GrpcServiceExample.Repositories;
using GrpcServiceExample.Repositories.db;

var builder = WebApplication.CreateBuilder(args);

//Setting for dapper
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Add services to the container.
builder.Services.AddGrpc();

// Repository
builder.Services.AddScoped<IWarehouseDb, WarehouseDb>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

var app = builder.Build();

builder.Configuration.AddEnvironmentVariables();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
