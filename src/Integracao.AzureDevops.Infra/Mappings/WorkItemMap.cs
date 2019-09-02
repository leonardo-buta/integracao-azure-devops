using Integracao.AzureDevops.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integracao.AzureDevops.Infra.Mappings
{
    public class WorkItemMap : IEntityTypeConfiguration<AzureWorkItem>
    {
        public void Configure(EntityTypeBuilder<AzureWorkItem> builder)
        {
            builder.ToTable("WorkItem");

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.HasIndex(x => x.WorkItemId).IsUnique();

            builder.Property(x => x.Titulo)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .IsRequired();
        }
    }
}
