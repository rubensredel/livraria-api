using AutoMapper;
using RR.Livraria.Domain.Interfaces.Notification;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.Models;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Application.Services;

public class VendaService : IVendaService
{
    private readonly IMapper _mapper;
    private readonly IVendaRepository _repository;
    private readonly IDomainNotification _domainNotification;
    private readonly ILivroVendaRepository _livroVendaRepository;

    public VendaService(IMapper mapper, IVendaRepository vendaRepository, IDomainNotification domainNotification, ILivroVendaRepository livroVendaRepository)
    {
        _mapper = mapper;
        _repository = vendaRepository;
        _domainNotification = domainNotification;
        _livroVendaRepository = livroVendaRepository;
    }

    public async Task<VendaViewModel> AddAsync(VendaNewViewModel venda)
    {
        var model = _mapper.Map<Venda>(venda);
        var codV = await _repository.AddAsync(model);
        var result = await _repository.GetByCodeAsync(codV);
        return _mapper.Map<VendaViewModel>(result);
    }

    public async Task<IEnumerable<VendaViewModel>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<VendaViewModel>>(result);
    }

    public async Task<VendaViewModel> UpdateAsync(VendaViewModel venda)
    {
        var model = _mapper.Map<Venda>(venda);
        await _repository.UpdateAsync(model);
        var result = await _repository.GetByCodeAsync(venda.CodV);
        return _mapper.Map<VendaViewModel>(result);
    }

    public async Task<bool> DeleteAsync(int code)
    {
        var livros = await _livroVendaRepository.GetByCodVAsync(code);
        if (livros.Any())
        {
            _domainNotification.AddNotification("Venda", "Não é possível excluir a venda, pois existem livros associados a ele.");
            return false;
        }
        var venda = await _repository.GetByCodeAsync(code);
        return await _repository.DeleteAsync(venda);
    }

    public async Task<VendaViewModel> GetByCodeAsync(int code)
    {
        var result = await _repository.GetByCodeAsync(code);
        return _mapper.Map<VendaViewModel>(result);
    }
}
