using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity GetByID(int id);

        void Add(TEntity item);
        void Save(TEntity item);
        void Delete(TEntity item);

        bool Exists(int id);
        bool Exists(TEntity item);
    }
}
