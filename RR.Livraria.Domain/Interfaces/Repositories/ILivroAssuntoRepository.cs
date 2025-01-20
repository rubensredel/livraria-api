using RR.Livraria.Domain.Models;

namespace RR.Livraria.Domain.Interfaces.Repositories;
public interface ILivroAssuntoRepository
{
    Task<bool> AddAsync(LivroAssunto livroAssunto);
    Task<bool> DeleteAsync(LivroAssunto livroAssunto);
    Task<IEnumerable<LivroAssunto>> GetByCodAsAsync(int code);
    Task<IEnumerable<LivroAssunto>> GetByCodlAsync(int code);
}
