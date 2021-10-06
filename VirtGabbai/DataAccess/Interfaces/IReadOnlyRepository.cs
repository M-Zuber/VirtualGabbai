using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccess.Interfaces
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Entities { get; }

        IEnumerable<TEntity> Get();
        TEntity GetById(int id);

        bool Exists(int id);
        bool Exists(TEntity item);
    }
}
