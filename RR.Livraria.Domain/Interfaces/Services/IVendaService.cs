using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Domain.Interfaces.Services;

public interface IVendaService
{
    Task<VendaViewModel> AddAsync(VendaNewViewModel venda);
    Task<IEnumerable<VendaViewModel>> GetAllAsync();
    Task<VendaViewModel> UpdateAsync(VendaViewModel venda);
    Task<bool> DeleteAsync(int code);
    Task<VendaViewModel> GetByCodeAsync(int code);
}
