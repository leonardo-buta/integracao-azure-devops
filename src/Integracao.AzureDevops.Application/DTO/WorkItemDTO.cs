using Integracao.AzureDevops.Domain.Enum;
using System;

namespace Integracao.AzureDevops.Application.DTO
{
    public class WorkItemDTO
    {
        public int WorkItemId { get; set; }
        public AzureWorkItemTipoEnum Tipo { get; set; }
        public string TipoString
        {
            get
            {
                return Tipo.ToString();
            }
        }
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Pagina { get; set; }
        public string Order { get; set; }
    }
}
