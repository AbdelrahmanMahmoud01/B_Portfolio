namespace Profile.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;

    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _context.Set<T>().ToListAsync();

    public async Task<T> GetByIdAsync(int id) =>
        await _context.Set<T>().FindAsync(id);

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public T UpdateAsync(T entity)
    {
        _context.Update(entity);
        return entity;
    } 

    public T DeleteAsync(T entity)
    {
        _context.Remove(entity);
        return entity;
    }
}
