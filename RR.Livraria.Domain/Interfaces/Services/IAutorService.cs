using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Domain.Interfaces.Services;

public interface IAutorService
{
    Task<AutorViewModel> AddAsync(AutorNewViewModel autor);
    Task<IEnumerable<AutorViewModel>> GetAllAsync();
    Task<AutorViewModel> GetByCodeAsync(int code);
    Task<bool> RemoveAsync(int code);
    Task<AutorViewModel> UpdateAsync(AutorViewModel autor);
}
