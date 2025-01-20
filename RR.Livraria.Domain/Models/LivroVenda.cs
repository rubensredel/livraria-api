namespace RR.Livraria.Domain.Models;

public record LivroVenda
{
    public int Livro_Codl { get; set; }
    public int Venda_CodV { get; set; }
    public decimal Valor { get; set; }
}
