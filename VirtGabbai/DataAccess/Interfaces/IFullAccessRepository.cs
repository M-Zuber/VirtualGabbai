namespace DataAccess.Interfaces
{
    public interface IFullAccessRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        void Add(TEntity item);
        void Save(TEntity item);
        void Delete(TEntity item);
    }
}
