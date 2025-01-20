namespace RR.Livraria.Domain.ViewModels;

public record LivroNewViewModel
{
    public string? Titulo { get; set; }
    public string? Editora { get; set; }
    public int? Edicao { get; set; }
    public string? AnoPublicacao { get; set; }
    public IList<AutorViewModel>? Autores { get; set; }
    public IList<AssuntoViewModel>? Assuntos { get; set; }
    public IList<LivroVendaViewModel>? Precos { get; set; }
}
