using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var repositoryCommandExecuter = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(IRepositoryCommandExecuter));
                if (repositoryCommandExecuter != null)
                {
                    services.Remove(repositoryCommandExecuter);
                }
                
                services.AddTransient<IRepositoryCommandExecuter, RepositoryMemoryCommandExecuter>();
            });
        }
        
    }
}