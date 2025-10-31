using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Infrastructure.Repositories;

public class PagamentoRepository : InMemoryRepository<Pagamento>, IRepository<Pagamento>
{
    public PagamentoRepository() : base(p => p.Id)
    {
        SeedData();
    }

    private void SeedData()
    {
        // Seed data será criado via UnitOfWork quando necessário
    }
}

