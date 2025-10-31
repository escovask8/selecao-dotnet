using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Infrastructure.Repositories;

public class CursoRepository : InMemoryRepository<Curso>, IRepository<Curso>
{
    public CursoRepository() : base(c => c.Id)
    {
        SeedData();
    }

    private void SeedData()
    {
        // Dados iniciais para testes
        var curso1 = new Curso(
            "ASP.NET Core Avançado",
            "Curso completo sobre ASP.NET Core com Clean Architecture",
            299.90m,
            40,
            "Prof. Carlos Mendes");

        var curso2 = new Curso(
            "Angular Moderno",
            "Desenvolvimento de aplicações modernas com Angular",
            249.90m,
            35,
            "Prof. Ana Paula");

        var curso3 = new Curso(
            "SOLID Principles",
            "Princípios SOLID e Clean Code na prática",
            199.90m,
            20,
            "Prof. Roberto Lima");

        _dataStore[curso1.Id] = curso1;
        _dataStore[curso2.Id] = curso2;
        _dataStore[curso3.Id] = curso3;
    }
}

