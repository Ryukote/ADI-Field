namespace Domain;

public interface IService<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<ICollection<T>> GetAllAsync();
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
