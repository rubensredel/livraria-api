using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Domain.Interfaces.Services;

public interface IAssuntoService
{
    Task<AssuntoViewModel> AddAsync(AssuntoNewViewModel assunto);
    Task<IEnumerable<AssuntoViewModel>> GetAllAsync();
    Task<AssuntoViewModel> GetByCodeAsync(int code);
    Task<bool> RemoveAsync(int code);
    Task<AssuntoViewModel> UpdateAsync(AssuntoViewModel assunto);
}
