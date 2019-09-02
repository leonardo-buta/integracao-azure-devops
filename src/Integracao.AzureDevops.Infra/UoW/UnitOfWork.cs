using Integracao.AzureDevops.Domain.Interfaces;
using Integracao.AzureDevops.Infra.Context;

namespace Integracao.AzureDevops.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IntegracaoContext _context;

        public UnitOfWork(IntegracaoContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
