using ClientModule;

namespace WebApi;

public static class Middlewares
{
    public static WebApplication UseEndpoints(this WebApplication app)
    {
        app
            .AddClientCommands()
            .MapControllers();
        
        return app;
    }
    
    public static WebApplication UseOpenApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}