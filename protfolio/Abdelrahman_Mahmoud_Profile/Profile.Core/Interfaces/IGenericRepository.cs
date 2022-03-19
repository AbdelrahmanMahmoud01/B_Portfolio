namespace Profile.Core.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    T UpdateAsync(T entity);
    T DeleteAsync(T entity);
}
