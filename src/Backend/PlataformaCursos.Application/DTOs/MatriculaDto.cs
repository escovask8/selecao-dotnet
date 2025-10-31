namespace PlataformaCursos.Application.DTOs;

public class MatriculaDto
{
    public Guid Id { get; set; }
    public Guid EstudanteId { get; set; }
    public Guid CursoId { get; set; }
    public DateTime DataMatricula { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? NomeEstudante { get; set; }
    public string? TituloCurso { get; set; }
}

