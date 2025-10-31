using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Domain.Entities;

namespace PlataformaCursos.Infrastructure.Services;

public interface IMetodoPagamento
{
    Task<Result<Pagamento>> ProcessarPagamento(decimal valor, string descricao, Guid estudanteId, Guid? cartaoCreditoId = null);
}

public class CartaoCreditoService : IMetodoPagamento
{
    public async Task<Result<Pagamento>> ProcessarPagamento(decimal valor, string descricao, Guid estudanteId, Guid? cartaoCreditoId = null)
    {
        // Mock de processamento de pagamento
        await Task.Delay(100); // Simula chamada assíncrona

        var pagamento = new Pagamento(estudanteId, valor, descricao, cartaoCreditoId);
        
        // Simula aprovação automática (em produção seria integração com gateway)
        pagamento.Aprovar();

        return Result<Pagamento>.Success(pagamento);
    }
}

