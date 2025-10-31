namespace PlataformaCursos.Application.DTOs;

public class EstudanteDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
    public string Status { get; set; } = string.Empty;
}

