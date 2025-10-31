using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Em memória, não há persistência real
        // Retorna 1 para simular sucesso
        return Task.FromResult(1);
    }
}

