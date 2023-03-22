using Eventuous.AspNetCore.Web;
using Serilog;
using Serilog.Events;
using WebApi;

var serilogLogger =
    new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
        .MinimumLevel.Override("Grpc", LogEventLevel.Information)
        .MinimumLevel.Override("EventStore", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console(
            outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}"
        )
        .CreateLogger();

Log.Logger = serilogLogger;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var configuration = builder.Configuration;
var services = builder.Services;

services
    .AddEndpointsApiExplorer()
    .AddCore(configuration)
    .AddModules(configuration)
    .AddOpenApi()
    .AddControllers();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseEventuousLogs();
        
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseOpenApi();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints();
app.Run();