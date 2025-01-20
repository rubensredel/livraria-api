using AutoMapper;
using RR.Livraria.Domain.Interfaces.Notification;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.Models;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Application.Services;

public class AutorService : IAutorService
{
    private readonly IMapper _mapper;
    private readonly IAutorRepository _repository;
    private readonly IDomainNotification _domainNotification;
    private readonly ILivroAutorRepository _livroAutorRepository;

    public AutorService(IMapper mapper, IAutorRepository repository, IDomainNotification domainNotification, ILivroAutorRepository livroAutorRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _domainNotification = domainNotification;
        _livroAutorRepository = livroAutorRepository;
    }

    public async Task<AutorViewModel> AddAsync(AutorNewViewModel autor)
    {
        var model = _mapper.Map<Autor>(autor);
        var codAu = await _repository.AddAsync(model);
        var result = await _repository.GetByCodeAsync(codAu);
        return _mapper.Map<AutorViewModel>(result);
    }

    public async Task<IEnumerable<AutorViewModel>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<AutorViewModel>>(result);
    }

    public async Task<AutorViewModel> UpdateAsync(AutorViewModel autor)
    {
        var model = _mapper.Map<Autor>(autor);
        await _repository.UpdateAsync(model);
        var result = await _repository.GetByCodeAsync(autor.CodAu);
        return _mapper.Map<AutorViewModel>(result);
    }

    public async Task<bool> RemoveAsync(int code)
    {
        var livros = await _livroAutorRepository.GetByCodAuAsync(code);
        if (livros.Any())
        {
            _domainNotification.AddNotification("Autor", "Não é possível excluir o autor, pois existem livros associados a ele.");
            return false;
        }
        var autor = await _repository.GetByCodeAsync(code);
        return await _repository.DeleteAsync(autor);
    }

    public async Task<AutorViewModel> GetByCodeAsync(int code)
    {
        var result = await _repository.GetByCodeAsync(code);
        return _mapper.Map<AutorViewModel>(result);
    }
}
