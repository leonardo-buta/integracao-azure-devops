using Integracao.AzureDevops.Domain.Interfaces;
using Integracao.AzureDevops.Infra.Context;
using System.Collections.Generic;
using System.Linq;

namespace Integracao.AzureDevops.Infra.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IntegracaoContext _db;

        public Repository(IntegracaoContext context) => _db = context;

        public void Add(TEntity obj) => _db.Set<TEntity>().Add(obj);

        public void AddMany(IEnumerable<TEntity> listObj) => _db.Set<TEntity>().AddRange(listObj);

        public TEntity GetById(int id) => _db.Set<TEntity>().Find(id);

        public IQueryable<TEntity> GetAll() => _db.Set<TEntity>();

        public void Update(TEntity obj) => _db.Set<TEntity>().Update(obj);

        public void Remove(int id) => _db.Set<TEntity>().Remove(GetById(id));

        public int SaveChanges() => _db.SaveChanges();
    }
}
