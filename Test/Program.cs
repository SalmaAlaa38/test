
using Application.Users.Endpoints;
using Application.Extension;
using Infrastructure.Extensions;
using IdentityProvider.Middlewares;
using Hangfire;
using Microsoft.Extensions.Configuration;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//GlobalExceptionHandler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddServicesLifeTime();
builder.Services.AddServices(builder.Configuration);
//HangFire
builder.Services.AddHangfire(x =>
    x.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("HangfireConnection")
        ?? throw new Exception("Connection string 'HangfireConnection' not found."))
);
builder.Services.AddHangfireServer(
    s => s.SchedulePollingInterval = TimeSpan.FromSeconds(15)
    );


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.MapUserEndpoints();

app.Run();


