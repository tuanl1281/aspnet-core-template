namespace Gateway.Application.Extensions;

public static class StartupExtension
{
    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(_ => _.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
    }
}