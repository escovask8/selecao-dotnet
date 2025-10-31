using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Infrastructure.Repositories;

public class EstudanteRepository : InMemoryRepository<Estudante>, IRepository<Estudante>
{
    public EstudanteRepository() : base(e => e.Id)
    {
        SeedData();
    }

    private void SeedData()
    {
        // Dados iniciais para testes
        var estudante1 = new Estudante("João Silva", new Domain.ValueObjects.Email("joao@example.com"));
        var estudante2 = new Estudante("Maria Santos", new Domain.ValueObjects.Email("maria@example.com"));
        
        _dataStore[estudante1.Id] = estudante1;
        _dataStore[estudante2.Id] = estudante2;
    }
}

