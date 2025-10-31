using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Infrastructure.Repositories;

public class CartaoCreditoRepository : InMemoryRepository<CartaoCredito>, IRepository<CartaoCredito>
{
    public CartaoCreditoRepository() : base(c => c.Id) { }
}

