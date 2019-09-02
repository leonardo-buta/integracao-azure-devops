namespace Integracao.AzureDevops.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
