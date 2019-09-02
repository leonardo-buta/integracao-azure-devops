using AutoMapper;
using Integracao.AzureDevops.Application.DTO;
using Integracao.AzureDevops.Application.Interfaces;
using Integracao.AzureDevops.Domain.Enum;
using Integracao.AzureDevops.Domain.Interfaces;
using Integracao.AzureDevops.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracao.AzureDevops.Application.Services
{
    public class AzureWorkItemAppService : IAzureWorkItemAppService
    {
        private readonly IAzureWorkItemRepository _azureWorkItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public AzureWorkItemAppService(IUnitOfWork unitOfWork, IMapper mapper, IAzureWorkItemRepository azureWorkItemRepository)
        {
            _azureWorkItemRepository = azureWorkItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<WorkItemDTO> GetAll(WorkItemDTO workItemDTO)
        {
            int pagination = 30;

            var query = _azureWorkItemRepository.GetAll()
                            .Where(x => (string.IsNullOrEmpty(workItemDTO.Titulo) || x.Titulo.Contains(workItemDTO.Titulo)) &&
                            (workItemDTO.Tipo == AzureWorkItemTipoEnum.Todos || x.Tipo == workItemDTO.Tipo));

            if (workItemDTO.Order == "ASC")
            {
                query = query.OrderBy(x => x.DataCriacao);
            }
            else
            {
                query = query.OrderByDescending(x => x.DataCriacao);
            }

            if (workItemDTO.Pagina > 0)
            {
                query = query.Skip(pagination * (workItemDTO.Pagina - 1)).Take(pagination);
            }

            return _mapper.Map<List<WorkItemDTO>>(query.ToList());
        }

        public async Task SincronizarWorkItens()
        {
            var workItens = await _azureWorkItemRepository.ObterWorkItensAzure();

            foreach (var item in workItens)
            {
                if (!_azureWorkItemRepository.ExisteWorkItem(item.WorkItemId))
                {
                    _azureWorkItemRepository.Add(item);
                }
            }

            _unitOfWork.Commit();
        }

        public void AdicionarWorkItem(AzureWorkItem workItem)
        {
            _azureWorkItemRepository.Add(workItem);
        }

        public void AdicionarWorkItem(IEnumerable<AzureWorkItem> listWorkItem)
        {
            _azureWorkItemRepository.AddMany(listWorkItem);
        }
    }
}
