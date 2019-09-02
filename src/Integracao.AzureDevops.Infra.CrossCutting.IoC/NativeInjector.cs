using Integracao.AzureDevops.Application.Interfaces;
using Integracao.AzureDevops.Application.Services;
using Integracao.AzureDevops.Domain.Interfaces;
using Integracao.AzureDevops.Domain.Models;
using Integracao.AzureDevops.Infra.Context;
using Integracao.AzureDevops.Infra.Repository;
using Integracao.AzureDevops.Infra.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracao.AzureDevops.Infra.CrossCutting.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IAzureWorkItemAppService, AzureWorkItemAppService>();

            // Repository
            services.AddScoped<IAzureWorkItemRepository, AzureWorkItemRepository>();

            // Context
            services.AddScoped<IntegracaoContext>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Singleton
            services.AddSingleton(configuration.GetSection("DevOpsCredentials").Get<DevOpsCredentials>());
        }
    }
}
