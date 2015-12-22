using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IFullAccessRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        void Add(TEntity item);
        void Save(TEntity item);
        void Delete(TEntity item);
    }
}
