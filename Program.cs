using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendingEmails;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
//builder.Services.AddSwaggerGen();

/*var _loggrer = new LoggerConfiguration()
.WriteTo.File("C:\\ForLogger\\ApiLog.log",rollingInterval:RollingInterval.Day)
.CreateLogger();
builder.Logging.AddSerilog(_loggrer);
*/


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var loggerConfiguration = new LoggerConfiguration()
    .WriteTo.File(configuration["Logger:FilePath"], rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog(loggerConfiguration);




var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseHttpsRedirection();
app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

//dotnet publish -c Release -o ./publish





