namespace ApplicationLayer.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<int> AddAsync(T entity);
        public Task<int> UpdateAsync(T entity);
        public Task<int> DeleteAsync(int id);
    }
}