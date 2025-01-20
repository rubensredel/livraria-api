using RR.Livraria.Domain.Models;

namespace RR.Livraria.Domain.Interfaces.Repositories;

public interface ILivroVendaRepository
{
    Task<bool> AddAsync(LivroVenda model);
    Task<IEnumerable<LivroVenda>> GetByCodlAsync(int code);
    Task<IEnumerable<LivroVenda>> GetByCodVAsync(int code);
    Task<bool> DeleteAsync(LivroVenda model);
}
