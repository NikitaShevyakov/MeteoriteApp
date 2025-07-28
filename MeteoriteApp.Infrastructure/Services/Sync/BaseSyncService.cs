using MeteoriteApp.Infrastructure.Repositories;

namespace MeteoriteApp.Infrastructure.Services.Sync
{
    public abstract class BaseSyncService<T> 
        : ISyncService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly Func<T, string> _keySelector;
        private readonly Func<string, T> _entityFactory;

        public BaseSyncService(
            IRepository<T> repository,
            Func<T, string> keySelector,
            Func<string, T> entityFactory)
        {
            _repository = repository;
            _keySelector = keySelector;
            _entityFactory = entityFactory;
        }

        public async Task<List<T>> SyncAsync(
            HashSet<string> incomingKeys,
            CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetAllAsync(cancellationToken);
            var existingKeys = existing
                .Select(_keySelector)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var toAdd = incomingKeys
                .Except(existingKeys, StringComparer.OrdinalIgnoreCase)
                .Select(_entityFactory)
                .ToList();

            var toRemove = existing
                .Where(e => !incomingKeys.Contains(_keySelector(e), StringComparer.OrdinalIgnoreCase))
                .ToList();

            if (toAdd.Any())
                await _repository.AddAsync(toAdd, cancellationToken);

            if (toRemove.Any())
                await _repository.RemoveAsync(toRemove, cancellationToken);

            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
