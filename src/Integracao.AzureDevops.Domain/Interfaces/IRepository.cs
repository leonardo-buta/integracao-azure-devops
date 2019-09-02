using System.Collections.Generic;
using System.Linq;

namespace Integracao.AzureDevops.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void AddMany(IEnumerable<TEntity> listObj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(int id);
        int SaveChanges();
    }
}
