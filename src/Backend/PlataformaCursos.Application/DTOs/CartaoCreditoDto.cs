namespace PlataformaCursos.Application.DTOs;

public class CartaoCreditoDto
{
    public Guid Id { get; set; }
    public Guid EstudanteId { get; set; }
    public string Numero { get; set; } = string.Empty;
    public string NomeTitular { get; set; } = string.Empty;
    public DateTime Validade { get; set; }
    public string CVV { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}

public class CadastrarCartaoCreditoDto
{
    public string Numero { get; set; } = string.Empty;
    public string NomeTitular { get; set; } = string.Empty;
    public DateTime Validade { get; set; }
    public string CVV { get; set; } = string.Empty;
}

