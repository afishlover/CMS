namespace ApplicationLayer.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T?> GetById(Guid id);
        public Task<IEnumerable<T>> GetAll();
        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(Guid id);
    }
}