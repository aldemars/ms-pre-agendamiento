using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ms_pre_agendamiento
{
    public class Program
    {
        
        private static string GetKeyVaultEndpoint() => "https://dev-pre-agendamiento.vault.azure.net/";
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureAppConfiguration((hostingContext, config) => {
                    var settings = config.Build();
                    var appConfig = settings["ConnectionStrings:AppConfig"];
                    if (!String.IsNullOrEmpty(appConfig))
                    {
                        config.AddAzureAppConfiguration(options => {
                            options.Connect(appConfig)
                                .UseFeatureFlags();
                        });
                    }
                });
        }
    }
}