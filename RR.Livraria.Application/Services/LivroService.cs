using System.Data;
using AutoMapper;
using ClosedXML.Excel;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.Models;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.Application.Services;

public class LivroService : ILivroService
{
    private readonly IMapper _mapper;
    private readonly ILivroRepository _repository;
    private readonly ILivroAutorRepository _livroAutorRepository;
    private readonly ILivroAssuntoRepository _livroAssuntoRepository;
    private readonly ILivroVendaRepository _livroVendaRepository;
    private readonly IAssuntoService _assuntoService;
    private readonly IAutorService _autorService;
    private readonly IVendaService _vendaService;

    public LivroService(
        IMapper mapper, 
        ILivroRepository repository, 
        ILivroAssuntoRepository livroAssuntoRepository, 
        ILivroAutorRepository livroAutorRepository,
        ILivroVendaRepository livroVendaRepository,
        IAssuntoService assuntoService,
        IAutorService autorService,
        IVendaService vendaService)
    {
        _mapper = mapper;
        _repository = repository;
        _livroAutorRepository = livroAutorRepository;
        _livroAssuntoRepository = livroAssuntoRepository;
        _livroVendaRepository = livroVendaRepository;
        _assuntoService = assuntoService;
        _autorService = autorService;
        _vendaService = vendaService;
    }

    public async Task<IEnumerable<LivroViewModel>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return await LoadDataAsync(result);
    }

    public async Task<LivroViewModel> AddAsync(LivroNewViewModel livro)
    {
        var model = _mapper.Map<Livro>(livro);
        var codl = await _repository.AddAsync(model);
        var result = await _repository.GetByCodeAsync(codl);
        await AddAutoresAsync(livro.Autores, codl);
        await AddAssuntosAsync(livro.Assuntos, codl);
        await AddPrecosAsync(livro.Precos, codl);
        return await LoadDataAsync(result);
    }

    protected async Task AddAutoresAsync(IList<AutorViewModel>? autores, int codl, bool deleteBefore = false)
    {
        if (deleteBefore)
            await DeleteAutoresAsync(codl);
        if (autores == null)
            return;
        foreach (var autor in autores)
        {
            var autorModel = new LivroAutor { Autor_CodAu = autor.CodAu, Livro_Codl = codl };
            await _livroAutorRepository.AddAsync(autorModel);
        }
    }

    protected async Task DeleteAutoresAsync(int codl)
    {
        var exAutores = await _livroAutorRepository.GetByCodlAsync(codl);
        foreach (var ex in exAutores)
            await _livroAutorRepository.DeleteAsync(ex);
    }

    protected async Task AddAssuntosAsync(IList<AssuntoViewModel>? assuntos, int codl, bool deleteBefore = false)
    {
        if (deleteBefore)
            await DeleteAssuntosAsync(codl);
        if(assuntos == null)
            return;
        foreach (var assunto in assuntos)
        {
            var assuntoModel = new LivroAssunto { Assunto_CodAs = assunto.CodAs, Livro_Codl = codl };
            await _livroAssuntoRepository.AddAsync(assuntoModel);
        }
    }
    
    protected async Task DeleteAssuntosAsync(int codl)
    {
        var exAssuntos = await _livroAssuntoRepository.GetByCodlAsync(codl);
        foreach (var ex in exAssuntos)
            await _livroAssuntoRepository.DeleteAsync(ex);
    }

    protected async Task AddPrecosAsync(IList<LivroVendaViewModel>? precos, int codl, bool deleteBefore = false)
    {
        if (deleteBefore)
            await DeletePrecosAsync(codl);
        if (precos == null)
            return;
        foreach (var preco in precos)
        {
            var livroVenda = new LivroVenda { Venda_CodV = preco.CodV, Livro_Codl = codl, Valor = preco.Valor };
            await _livroVendaRepository.AddAsync(livroVenda);
        }
    }

    protected async Task DeletePrecosAsync(int codl)
    {
        var exPrecos = await _livroVendaRepository.GetByCodlAsync(codl);
        foreach (var ex in exPrecos)
            await _livroVendaRepository.DeleteAsync(ex);
    }

    public async Task<LivroViewModel> GetByCodeAsync(int code)
    {
        var result = await _repository.GetByCodeAsync(code);
        return await LoadDataAsync(result);
    }

    public async Task<LivroViewModel> UpdateAsync(LivroViewModel livro)
    {
        var model = _mapper.Map<Livro>(livro);
        await _repository.UpdateAsync(model);
        var result = await _repository.GetByCodeAsync(livro.Codl);
        await AddAutoresAsync(livro.Autores, livro.Codl, true);
        await AddAssuntosAsync(livro.Assuntos, livro.Codl, true);
        await AddPrecosAsync(livro.Precos, livro.Codl, true);
        return await LoadDataAsync(result);
    }

    public async Task<bool> RemoveAsync(int code)
    {
        await DeleteAutoresAsync(code);
        await DeleteAssuntosAsync(code);
        await DeletePrecosAsync(code);
        var livro = await _repository.GetByCodeAsync(code);
        return await _repository.DeleteAsync(livro);
    }

    protected async Task<IEnumerable<LivroViewModel>> LoadDataAsync(IEnumerable<Livro>? livros)
    {
        var result = new List<LivroViewModel>();
        if (livros == null)
            return result;
        foreach(var livro in livros)
            result.Add(await LoadDataAsync(livro));
        return result;
    }

    protected async Task<LivroViewModel> LoadDataAsync(Livro livro)
    {
        var result = _mapper.Map<LivroViewModel>(livro);
        var autores = await _livroAutorRepository.GetByCodlAsync(livro.Codl);
        result.Autores = [];
        foreach (var autor in autores)
        {
            var autorModel = await _autorService.GetByCodeAsync(autor.Autor_CodAu);
            result.Autores.Add(autorModel);
        }
        var assuntos = await _livroAssuntoRepository.GetByCodlAsync(livro.Codl);
        result.Assuntos = [];
        foreach (var assunto in assuntos)
        {
            var assuntoModel = await _assuntoService.GetByCodeAsync(assunto.Assunto_CodAs);
            result.Assuntos.Add(assuntoModel);
        }
        var precos = await _livroVendaRepository.GetByCodlAsync(livro.Codl);
        result.Precos = [];
        foreach (var preco in precos)
        {
            var venda = await _vendaService.GetByCodeAsync(preco.Venda_CodV);
            var precoModel = new LivroVendaViewModel { CodV = venda.CodV, Descricao = venda.Descricao, Valor = preco.Valor };
            result.Precos.Add(precoModel);
        }
        return result;
    }

    public async Task<XLWorkbook> GetReportAsync()
    {
        var report = await _repository.GetReportAsync();

        var dataTable = new DataTable()
        {
            TableName = "LivrosEAutoresData"
        };
        dataTable.Columns.Add("Cd Autor", typeof(int));
        dataTable.Columns.Add("Nome", typeof(string));
        dataTable.Columns.Add("Cd Livro", typeof(int));
        dataTable.Columns.Add("Título", typeof(string));
        dataTable.Columns.Add("Editora", typeof(string));
        dataTable.Columns.Add("Edicao", typeof(int));
        dataTable.Columns.Add("Ano Publicação", typeof(string));
        dataTable.Columns.Add("Assuntos", typeof(string));
        foreach (var item in report)
        {
            DataRow row = dataTable.NewRow();
            row["Cd Autor"] = item.CodAu;
            row["Nome"] = item.Nome;
            row["Cd Livro"] = item.Codl;
            row["Título"] = item.Titulo;
            row["Editora"] = item.Editora;
            row["Edicao"] = item.Edicao;
            row["Ano Publicação"] = item.AnoPublicacao;
            row["Assuntos"] = item.Assuntos;
            dataTable.Rows.Add(row);
        }
        dataTable.AcceptChanges();

        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(dataTable);
        worksheet.Columns().AdjustToContents();
        return workbook;
    }
}
