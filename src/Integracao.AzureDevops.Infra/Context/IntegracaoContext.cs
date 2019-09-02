using Integracao.AzureDevops.Domain.Models;
using Integracao.AzureDevops.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Integracao.AzureDevops.Infra.Context
{
    public class IntegracaoContext : DbContext
    {
        public IntegracaoContext(DbContextOptions<IntegracaoContext> options) : base(options)
        {
            
        }

        public DbSet<AzureWorkItem> WorkItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkItemMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
