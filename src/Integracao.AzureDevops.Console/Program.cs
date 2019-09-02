using Integracao.AzureDevops.Application.Interfaces;
using Integracao.AzureDevops.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Integracao.AzureDevops.ConsoleServico
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleStartup startup = new ConsoleStartup();
            startup.Load();

            var workItemservice = startup.ServiceProvider.GetService<IAzureWorkItemAppService>();

            try
            {
                await workItemservice.SincronizarWorkItens();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
