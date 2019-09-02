using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integracao.AzureDevops.Application.DTO;
using Integracao.AzureDevops.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Integracao.AzureDevops.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureWorkItemController : ControllerBase
    {
        private readonly IAzureWorkItemAppService _azureWorkItemAppService;

        public AzureWorkItemController(IAzureWorkItemAppService azureWorkItemAppService)
        {
            _azureWorkItemAppService = azureWorkItemAppService;
        }

        [HttpGet]
        [Route("ObterListaWorkItens")]
        public IActionResult Get([FromQuery] WorkItemDTO workItemDTO)
        {
            try
            {
                var workItens = _azureWorkItemAppService.GetAll(workItemDTO);

                return Ok(workItens);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}