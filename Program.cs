using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MvcMovie.Models;
using System;

namespace MvcMovie
{
    /**
     * On startup, an ASP.NET Core app builds a host. The host encapsulates all of the app's resources, such as:
     * 
     *  - An HTTP server implementation
     *  - Middleware components
     *  - Logging
     *  - DI services
     *  - Configuration
     *
     * The code below creates a .NET Generic Host
     */
    public class Program
    {
        /**
         * 
         */
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();

        }

        /**
         * CreateDefaultBuilder provides default configuration for the app in the following order:
         * 
         * - ChainedConfigurationProvider : Adds an existing IConfiguration as a source. In the default
         *   configuration case, adds the host configuration and sets it as the first source for the app
         *   configuration.
         * 
         * - appsettings.json using the JSON configuration provider.
         * 
         * - appsettings.Environment.json using the JSON configuration provider. For example, appsettings.Production.json and appsettings.Development.json.
         * 
         * - App secrets when the app runs in the Development environment.
         * 
         * - Environment variables using the Environment Variables configuration provider.
         * 
         * - Command-line arguments using the Command-line configuration provider.
         * 
         * Configuration providers that are added later override previous key settings. For example, if MyKey
         * is set in both appsettings.json and the environment, the environment value is used. Using the
         * default configuration providers, the Command-line configuration provider overrides all other providers.
         *
         */
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // The host provides services that are available to the Startup class constructor. The app adds additional
            // services via ConfigureServices. Both the host and app services are available in Configure throughout the
            // app.
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // The Startup class is specified when the app's host is built. The startup class is specified by
                    // calling the WebHostBuilderExtensions.UseStartup<TStartup> method on the host builder.
                    webBuilder.UseStartup<Startup>();
                });
    }
}