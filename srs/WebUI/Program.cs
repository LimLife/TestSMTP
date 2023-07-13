using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repository;
using Application.Interfaces;
using Application.Services;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddScoped<Application.Interfaces.ILogger, Logger>();
builder.Services.AddTransient<IEmailService, EmailService>();


builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfiguration").Get<SMTP>());

builder.Services.AddDbContext<DBData>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("MessageDataBase"));
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();


app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseDefaultFiles();
app.UseStaticFiles();


app.Run();