using Dapper;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Models;
using RR.Livraria.Infra.Contexts;

namespace RR.Livraria.Infra.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly DapperContext _dapperContext;

    public LivroRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<int> AddAsync(Livro livro)
    {
        var query = @"
            INSERT INTO [dbo].[Livro]
                ([Titulo], [Editora], [Edicao], [AnoPublicacao])
            OUTPUT
                Inserted.Codl
            VALUES 
                (@Titulo, @Editora, @Edicao, @AnoPublicacao)";

        var result = await _dapperContext.DapperConnection.QuerySingleAsync<int>(query, livro);
        return result;
    }

    public async Task<IEnumerable<Livro>> GetAllAsync()
    {
        var query = "SELECT * FROM [dbo].[Livro]";
        var result = await _dapperContext.DapperConnection.QueryAsync<Livro>(query);
        return result;
    }

    public async Task<bool> UpdateAsync(Livro livro)
    {
        var query = @"
            UPDATE 
                [dbo].[Livro]
            SET
                [Titulo] = @Titulo,
                [Editora] = @Editora,
                [Edicao] = @Edicao,
                [AnoPublicacao] = @AnoPublicacao
            WHERE
                [Codl] = @Codl";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, livro);
        return (result > 0);
    }

    public async Task<bool> DeleteAsync(Livro livro)
    {
        var query = "DELETE FROM [dbo].[Livro] WHERE [Codl] = @Codl";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, livro);
        return (result > 0);
    }

    public async Task<Livro> GetByCodeAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Livro] WHERE [Codl] = @Code";
        var result = await _dapperContext.DapperConnection.QueryFirstOrDefaultAsync<Livro>(query, new { Code = code });
        return result;
    }
}
