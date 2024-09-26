using GrpcServiceExample.Mapper;
using GrpcServiceExample.Repositories;
using GrpcServiceExample.Repositories.db;
using GrpcServiceExample.Services;
using GrpcServiceExample.UseCase;

var builder = WebApplication.CreateBuilder(args);

//Setting for dapper
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;


// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repository
builder.Services.AddScoped<IWarehouseDb, WarehouseDb>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IWarehouseUseCase, WarehouseUseCase>(); 

var app = builder.Build();

builder.Configuration.AddEnvironmentVariables();

// Configure the HTTP request pipeline.
app.MapGrpcService<WarehouseService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
