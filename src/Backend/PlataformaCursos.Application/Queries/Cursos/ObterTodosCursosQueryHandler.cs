using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PlataformaCursos.Application.DTOs;
using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Application.Queries.Cursos;

public class ObterTodosCursosQueryHandler : IRequestHandler<ObterTodosCursosQuery, IEnumerable<CursoDto>>
{
    private readonly IRepository<Curso> _repository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private const string CACHE_KEY = "cursos_todos";

    public ObterTodosCursosQueryHandler(
        IRepository<Curso> repository,
        IMapper mapper,
        IMemoryCache cache)
    {
        _repository = repository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IEnumerable<CursoDto>> Handle(ObterTodosCursosQuery request, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(CACHE_KEY, out IEnumerable<CursoDto>? cachedCursos) && cachedCursos != null)
            return cachedCursos;

        var cursos = await _repository.FindAsync(c => c.Ativo, cancellationToken);
        var dtos = _mapper.Map<IEnumerable<CursoDto>>(cursos);

        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };

        _cache.Set(CACHE_KEY, dtos, cacheOptions);

        return dtos;
    }
}

