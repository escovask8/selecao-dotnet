using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Infrastructure.Repositories;

public class MatriculaRepository : InMemoryRepository<Matricula>, IRepository<Matricula>
{
    public MatriculaRepository() : base(m => m.Id) { }
}

