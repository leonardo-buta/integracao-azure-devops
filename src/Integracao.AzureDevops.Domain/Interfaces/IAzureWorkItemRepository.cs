using Integracao.AzureDevops.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracao.AzureDevops.Domain.Interfaces
{
    public interface IAzureWorkItemRepository : IRepository<AzureWorkItem>
    {
        Task<List<AzureWorkItem>> ObterWorkItensAzure();
        bool ExisteWorkItem(int idWorkItem);
    }
}
