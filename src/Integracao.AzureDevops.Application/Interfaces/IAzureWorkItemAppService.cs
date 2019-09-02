using Integracao.AzureDevops.Application.DTO;
using Integracao.AzureDevops.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracao.AzureDevops.Application.Interfaces
{
    public interface IAzureWorkItemAppService
    {
        Task SincronizarWorkItens();
        IEnumerable<WorkItemDTO> GetAll(WorkItemDTO workItemDTO);
    }
}
