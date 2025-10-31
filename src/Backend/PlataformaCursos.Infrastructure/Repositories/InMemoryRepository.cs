using System.Linq.Expressions;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Infrastructure.Repositories;

public class InMemoryRepository<T> : IRepository<T> where T : class
{
    protected readonly Dictionary<Guid, T> _dataStore = new();
    private readonly Func<T, Guid> _getId;

    public InMemoryRepository(Func<T, Guid> getId)
    {
        _getId = getId;
    }

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _dataStore.TryGetValue(id, out var entity);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<T>>(_dataStore.Values.ToList());
    }

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var compiled = predicate.Compile();
        var results = _dataStore.Values.Where(compiled).ToList();
        return Task.FromResult<IEnumerable<T>>(results);
    }

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var id = _getId(entity);
        _dataStore[id] = entity;
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var id = _getId(entity);
        if (_dataStore.ContainsKey(id))
        {
            _dataStore[id] = entity;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        var id = _getId(entity);
        _dataStore.Remove(id);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var compiled = predicate.Compile();
        var exists = _dataStore.Values.Any(compiled);
        return Task.FromResult(exists);
    }
}

