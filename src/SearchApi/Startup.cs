using Application.Core.Services;
using Application.Core.Interfaces;
using Application.Core.Interfaces.Repository;
using Repository;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<ApplicationDbContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<ISearchRepository, SearchRepository>();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        
    }
}
