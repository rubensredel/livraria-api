using AutoMapper;
using RR.Livraria.Domain.Interfaces.Notification;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.Models;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Application.Services;

public class AssuntoService : IAssuntoService
{
    private readonly IMapper _mapper;
    private readonly IAssuntoRepository _repository;
    private readonly IDomainNotification _domainNotification;
    private readonly ILivroAssuntoRepository _livroAssuntoRepository;
    public AssuntoService(IMapper mapper, IAssuntoRepository repository, IDomainNotification domainNotification, ILivroAssuntoRepository livroAssuntoRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _domainNotification = domainNotification;
        _livroAssuntoRepository = livroAssuntoRepository;
    }

    public async Task<AssuntoViewModel> AddAsync(AssuntoNewViewModel assunto)
    {
        var model = _mapper.Map<Assunto>(assunto);
        var codAs = await _repository.AddAsync(model);
        var result = await _repository.GetByCodeAsync(codAs);
        return _mapper.Map<AssuntoViewModel>(result);
    }

    public async Task<IEnumerable<AssuntoViewModel>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<AssuntoViewModel>>(result);
    }

    public async Task<AssuntoViewModel> UpdateAsync(AssuntoViewModel assunto)
    {
        var model = _mapper.Map<Assunto>(assunto);
        await _repository.UpdateAsync(model);
        var result = await _repository.GetByCodeAsync(assunto.CodAs);
        return _mapper.Map<AssuntoViewModel>(result);
    }

    public async Task<bool> RemoveAsync(int code)
    {
        var livros = await _livroAssuntoRepository.GetByCodAsAsync(code);
        if (livros.Any())
        {
            _domainNotification.AddNotification("Assunto", "Não é possível excluir o assunto, pois existem livros associados a ele.");
            return false;
        }
        var assunto = await _repository.GetByCodeAsync(code);
        return await _repository.DeleteAsync(assunto);
    }

    public async Task<AssuntoViewModel> GetByCodeAsync(int code)
    {
        var result = await _repository.GetByCodeAsync(code);
        return _mapper.Map<AssuntoViewModel>(result);
    }
}
