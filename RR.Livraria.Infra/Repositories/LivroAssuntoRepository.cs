using Dapper;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Models;
using RR.Livraria.Infra.Contexts;

namespace RR.Livraria.Infra.Repositories;

public class LivroAssuntoRepository : ILivroAssuntoRepository
{
    private readonly DapperContext _dapperContext;
    public LivroAssuntoRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<bool> AddAsync(LivroAssunto livroAssunto)
    {
        var query = @"
            INSERT INTO [dbo].[Livro_Assunto]
                ([Livro_Codl], [Assunto_CodAs])
            VALUES 
                (@Livro_Codl, @Assunto_CodAs)";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, livroAssunto);
        return result > 0;
    }

    public async Task<IEnumerable<LivroAssunto>> GetByCodlAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Livro_Assunto] WHERE Livro_Codl = @code";
        var result = await _dapperContext.DapperConnection.QueryAsync<LivroAssunto>(query, new { code });
        return result;
    }

    public async Task<IEnumerable<LivroAssunto>> GetByCodAsAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Livro_Assunto] WHERE Assunto_CodAs = @code";
        var result = await _dapperContext.DapperConnection.QueryAsync<LivroAssunto>(query, new { code });
        return result;
    }

    public async Task<bool> DeleteAsync(LivroAssunto livroAssunto)
    {
        var query = @"
            DELETE FROM 
                [dbo].[Livro_Assunto]
            WHERE 
                [Livro_Codl] = @Livro_Codl AND
                [Assunto_CodAs] = @Assunto_CodAs";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, livroAssunto);
        return (result > 0);
    }
}