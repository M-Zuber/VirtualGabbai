using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Entities { get; }

        IEnumerable<TEntity> Get();
        TEntity GetByID(int id);

        bool Exists(int id);
        bool Exists(TEntity item);
    }
}
