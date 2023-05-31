using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Stereograph.TechnicalTest.Api.Entities;
using System;
using System.IO;

namespace Stereograph.TechnicalTest.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });

        services.AddSwaggerGen(options =>
        {
            options.DescribeAllParametersInCamelCase();
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Stereograph.TechTechnique.Api", Version = "v1" });
            options.CustomSchemaIds(schema => schema.FullName);
        });

        services
            .AddControllers();
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            application
                .UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Stereograph.TechTechnique.Api V1"));
        }

        application
            .UseHttpsRedirection()
            .UseRouting()
            .UseCors()
            .UseEndpoints(endpoints => endpoints.MapControllers());

        using IServiceScope scope = application.ApplicationServices.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        ApplicationDbContext appDbContext = services.GetRequiredService<ApplicationDbContext>();
        appDbContext.Database.Migrate();
    }
}
