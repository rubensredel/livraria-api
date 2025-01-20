using AutoMapper;
using RR.Livraria.Domain.Models;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Application.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Livro, LivroViewModel>().ReverseMap();
        CreateMap<LivroNewViewModel, Livro>().ReverseMap();
        CreateMap<Autor, AutorViewModel>().ReverseMap();
        CreateMap<AutorNewViewModel, Autor>().ReverseMap();
        CreateMap<Assunto, AssuntoViewModel>().ReverseMap();
        CreateMap<AssuntoNewViewModel, Assunto>().ReverseMap();
        CreateMap<Venda, VendaViewModel>().ReverseMap();
        CreateMap<VendaNewViewModel, Venda>().ReverseMap();
    }
}
