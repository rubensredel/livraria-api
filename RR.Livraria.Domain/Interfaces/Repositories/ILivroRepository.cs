using RR.Livraria.Domain.Models;

namespace RR.Livraria.Domain.Interfaces.Repositories;

public interface ILivroRepository
{
    Task<int> AddAsync(Livro livro);
    Task<bool> DeleteAsync(Livro livro);
    Task<IEnumerable<Livro>> GetAllAsync();
    Task<bool> UpdateAsync(Livro livro);
    Task<Livro> GetByCodeAsync(int code);
}