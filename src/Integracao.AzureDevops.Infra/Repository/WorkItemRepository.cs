using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Integracao.AzureDevops.Domain.Interfaces;
using Integracao.AzureDevops.Domain.Models;
using Integracao.AzureDevops.Infra.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Common;
using System.Linq;
using Integracao.AzureDevops.Domain.Enum;

namespace Integracao.AzureDevops.Infra.Repository
{
    public class AzureWorkItemRepository : Repository<AzureWorkItem>, IAzureWorkItemRepository
    {
        private DevOpsCredentials _credenciais { get; set; }

        public AzureWorkItemRepository(IntegracaoContext context, DevOpsCredentials credenciais) : base(context)
        {
            _credenciais = credenciais;
        }

        public async Task<List<AzureWorkItem>> ObterWorkItensAzure()
        {
            List<AzureWorkItem> result = new List<AzureWorkItem>();
            Uri uri = new Uri(_credenciais.Uri);
            VssBasicCredential credentials = new VssBasicCredential("", _credenciais.Token);

            Wiql wiql = new Wiql()
            {
                Query = $@"SELECT [State], [Title]
                           FROM WorkItems
                           WHERE [System.TeamProject] = '{_credenciais.Projeto}'
                           ORDER BY [State] ASC, [Changed Date] Desc"
            };

            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                try
                {
                    WorkItemQueryResult workItemQueryResult = await workItemTrackingHttpClient.QueryByWiqlAsync(wiql);

                    if (workItemQueryResult.WorkItems.Count() != 0)
                    {
                        List<int> list = new List<int>();
                        list.AddRange(workItemQueryResult.WorkItems.Select(x => x.Id));

                        var colunas = new List<string>
                        {
                            "System.Id",
                            "System.WorkItemType",
                            "System.Title",
                            "System.CreatedDate"
                        };

                        var resultapi = await workItemTrackingHttpClient.GetWorkItemsAsync(list, colunas, workItemQueryResult.AsOf);

                        result = resultapi.Select(x => new AzureWorkItem
                        {
                            WorkItemId = x.Id.Value,
                            Tipo = (AzureWorkItemTipoEnum)Enum.Parse(typeof(AzureWorkItemTipoEnum), resultapi[0].Fields["System.WorkItemType"].ToString()),
                            Titulo = x.Fields["System.Title"].ToString(),
                            DataCriacao = Convert.ToDateTime(x.Fields["System.CreatedDate"])
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }

        public bool ExisteWorkItem(int idWorkItem)
        {
            return _db.WorkItem.Any(x => x.WorkItemId == idWorkItem);
        }
    }
}
