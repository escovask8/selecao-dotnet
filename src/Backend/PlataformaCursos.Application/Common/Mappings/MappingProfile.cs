using AutoMapper;
using PlataformaCursos.Application.Commands.Estudantes;
using PlataformaCursos.Application.DTOs;
using PlataformaCursos.Domain.Entities;

namespace PlataformaCursos.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Estudante, EstudanteDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));
        CreateMap<CartaoCredito, CartaoCreditoDto>();
        CreateMap<Curso, CursoDto>();
        CreateMap<Matricula, MatriculaDto>();
        CreateMap<Pagamento, PagamentoDto>();

        CreateMap<CadastrarEstudanteCommand, Estudante>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => new Domain.ValueObjects.Email(src.Email)))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Cartoes, opt => opt.Ignore())
            .ForMember(dest => dest.Matriculas, opt => opt.Ignore())
            .ForMember(dest => dest.Pagamentos, opt => opt.Ignore());
    }
}

