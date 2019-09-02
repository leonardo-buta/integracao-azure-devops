using Integracao.AzureDevops.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Integracao.AzureDevops.Infra.CrossCutting.IoC
{
    public class ConsoleStartup
    {
        public IConfiguration Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        public void Load()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IntegracaoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            NativeInjector.RegisterServices(services, Configuration);
        }
    }
}
