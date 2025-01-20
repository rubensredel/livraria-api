using RR.Livraria.Domain.Models;

namespace RR.Livraria.Domain.Interfaces.Repositories;

public interface IAssuntoRepository
{
    Task<int> AddAsync(Assunto assunto);
    Task<IEnumerable<Assunto>> GetAllAsync();
    Task<bool> UpdateAsync(Assunto assunto);
    Task<bool> DeleteAsync(Assunto assunto);
    Task<Assunto> GetByCodeAsync(int code);
}