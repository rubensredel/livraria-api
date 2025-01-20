using RR.Livraria.Domain.Models;

namespace RR.Livraria.Domain.Interfaces.Repositories;

public interface ILivroAutorRepository
{
    Task<bool> AddAsync(LivroAutor livroAutor);
    Task<IEnumerable<LivroAutor>> GetByCodlAsync(int code);
    Task<IEnumerable<LivroAutor>> GetByCodAuAsync(int code);
    Task<bool> DeleteAsync(LivroAutor livroAutor);
}
