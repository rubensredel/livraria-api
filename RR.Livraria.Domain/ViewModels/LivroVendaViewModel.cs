namespace RR.Livraria.Domain.ViewModels;

public record LivroVendaViewModel
{
    public int CodV { get; set; }
    public string? Descricao { get; set; }
    public decimal Valor { get; set; }
}
