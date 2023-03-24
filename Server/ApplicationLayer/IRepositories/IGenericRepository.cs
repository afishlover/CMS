namespace ApplicationLayer.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T?> GetById(Guid id);
        public Task<IEnumerable<T>> GetAll();
        public Task<int> Add(T entity);
        public Task<int> Update(T entity);
        public Task<int> Delete(Guid id);
    }
}