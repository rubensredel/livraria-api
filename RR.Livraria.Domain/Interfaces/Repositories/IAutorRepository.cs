using RR.Livraria.Domain.Models;

namespace RR.Livraria.Domain.Interfaces.Repositories;

public interface IAutorRepository
{
    Task<int> AddAsync(Autor autor);
    Task<IEnumerable<Autor>> GetAllAsync();
    Task<bool> UpdateAsync(Autor autor);
    Task<bool> DeleteAsync(Autor autor);
    Task<Autor> GetByCodeAsync(int code);
}
