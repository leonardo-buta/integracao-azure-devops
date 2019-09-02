using Integracao.AzureDevops.Domain.Enum;
using System;

namespace Integracao.AzureDevops.Domain.Models
{
    public class AzureWorkItem : Entity
    {
        public int WorkItemId { get; set; }
        public AzureWorkItemTipoEnum Tipo { get; set; }
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
