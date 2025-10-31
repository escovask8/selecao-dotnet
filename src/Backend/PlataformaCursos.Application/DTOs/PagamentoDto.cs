namespace PlataformaCursos.Application.DTOs;

public class PagamentoDto
{
    public Guid Id { get; set; }
    public Guid EstudanteId { get; set; }
    public Guid? CartaoCreditoId { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataPagamento { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}

