using Dapper;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Models;
using RR.Livraria.Infra.Contexts;

namespace RR.Livraria.Infra.Repositories;

public class LivroAutorRepository : ILivroAutorRepository
{
    private readonly DapperContext _dapperContext;
    public LivroAutorRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<bool> AddAsync(LivroAutor livroAutor)
    {
        var query = @"
            INSERT INTO [dbo].[Livro_Autor]
                ([Livro_Codl], [Autor_CodAu])
            VALUES 
                (@Livro_Codl, @Autor_CodAu)";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, livroAutor);
        return result > 0;
    }

    public async Task<IEnumerable<LivroAutor>> GetByCodlAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Livro_Autor] WHERE Livro_Codl = @code";
        var result = await _dapperContext.DapperConnection.QueryAsync<LivroAutor>(query, new { code });
        return result;
    }

    public async Task<IEnumerable<LivroAutor>> GetByCodAuAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Livro_Autor] WHERE Autor_CodAu = @code";
        var result = await _dapperContext.DapperConnection.QueryAsync<LivroAutor>(query, new { code });
        return result;
    }

    public async Task<bool> DeleteAsync(LivroAutor livroAutor)
    {
        var query = @"
            DELETE FROM 
                [dbo].[Livro_Autor]
            WHERE 
                [Livro_Codl] = @Livro_Codl AND
                [Autor_CodAu] = @Autor_CodAu";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, livroAutor);
        return (result > 0);
    }
}