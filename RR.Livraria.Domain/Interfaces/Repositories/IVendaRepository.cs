using RR.Livraria.Domain.Models;

namespace RR.Livraria.Domain.Interfaces.Repositories;

public interface IVendaRepository
{
    Task<int> AddAsync(Venda venda);
    Task<IEnumerable<Venda>> GetAllAsync();
    Task<bool> UpdateAsync(Venda venda);
    Task<bool> DeleteAsync(Venda venda);
    Task<Venda> GetByCodeAsync(int code);
}
