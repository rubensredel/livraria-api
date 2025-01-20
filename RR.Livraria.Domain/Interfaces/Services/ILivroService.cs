using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Domain.Interfaces.Services;

public interface ILivroService
{
    Task<IEnumerable<LivroViewModel>> GetAllAsync();
    Task<LivroViewModel> GetByCodeAsync(int code);
    Task<LivroViewModel> AddAsync(LivroNewViewModel model);
    Task<LivroViewModel> UpdateAsync(LivroViewModel model);
    Task<bool> RemoveAsync(int code);
}
