using Gateway.Application.Extensions;

namespace Gateway.Application;

public class Startup
{
    private IConfiguration Configuration { get; }

    private IWebHostEnvironment Environment { get; }

    public Startup(IWebHostEnvironment environment)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        Environment = environment;
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        /* For controller */
        services.AddControllers();
        /* For policy */
        services.AddCorsPolicy();
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            application.UseDeveloperExceptionPage();
        /* Policy */
        application.UseCors("AllowAll");
        /* For route */
        application.UseHttpsRedirection();
        application.UseRouting();
        /* For endpoint */
        application.UseEndpoints(_ =>  _.MapControllers());
    }
}