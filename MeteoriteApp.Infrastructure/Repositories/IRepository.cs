namespace MeteoriteApp.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(List<T> itemsToAdd, CancellationToken cancellationToken);
        Task RemoveAsync(List<T> itemsToRemove, CancellationToken cancellationToken);
    }
}
