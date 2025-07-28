namespace MeteoriteApp.Infrastructure.Services.Sync
{
    public interface ISyncService<T> where T : class
    {
        Task<List<T>> SyncAsync(
            HashSet<string> incomingKeys,
            CancellationToken cancellationToken = default);
    }
}
