namespace PlataformaCursos.Application.DTOs;

public class CursoDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Duracao { get; set; }
    public string Professor { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}

