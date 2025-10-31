using Microsoft.Extensions.DependencyInjection;
using PlataformaCursos.Application.Handlers;
using PlataformaCursos.Domain.Interfaces;
using PlataformaCursos.Infrastructure.Repositories;
using PlataformaCursos.Infrastructure.Services;

namespace PlataformaCursos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Repositories
        services.AddSingleton<IRepository<Domain.Entities.Estudante>, EstudanteRepository>();
        services.AddSingleton<IRepository<Domain.Entities.CartaoCredito>, CartaoCreditoRepository>();
        services.AddSingleton<IRepository<Domain.Entities.Curso>, CursoRepository>();
        services.AddSingleton<IRepository<Domain.Entities.Matricula>, MatriculaRepository>();
        services.AddSingleton<IRepository<Domain.Entities.Pagamento>, PagamentoRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, Infrastructure.UnitOfWork.UnitOfWork>();

        // Services
        services.AddScoped<IEmailService, MockEmailService>();
        services.AddScoped<IMetodoPagamento, CartaoCreditoService>();

        return services;
    }
}

