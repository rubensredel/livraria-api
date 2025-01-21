namespace RR.Livraria.Domain.Models;

public record Report
{
    public int CodAu { get; set; }
    public string Nome { get; set; }
    public int Codl { get; set; }
    public string Titulo { get; set; }
    public string Editora { get; set; }
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; }
    public string Assuntos { get; set; }
}
