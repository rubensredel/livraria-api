using Dapper;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Models;
using RR.Livraria.Infra.Contexts;

namespace RR.Livraria.Infra.Repositories;

public class AssuntoRepository : IAssuntoRepository
{
    private readonly DapperContext _dapperContext;
    public AssuntoRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<int> AddAsync(Assunto assunto)
    {
        var query = @"
            INSERT INTO [dbo].[Assunto]
                ([Descricao])
            OUTPUT
                Inserted.CodAs
            VALUES 
                (@Descricao)";
        var result = await _dapperContext.DapperConnection.QuerySingleAsync<int>(query, assunto);
        return result;
    }

    public async Task<IEnumerable<Assunto>> GetAllAsync()
    {
        var query = "SELECT * FROM [dbo].[Assunto]";
        var result = await _dapperContext.DapperConnection.QueryAsync<Assunto>(query);
        return result;
    }

    public async Task<bool> UpdateAsync(Assunto assunto)
    {
        var query = @"
            UPDATE 
                [dbo].[Assunto]
            SET
                [Descricao] = @Descricao
            WHERE
                [CodAs] = @CodAs";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, assunto);
        return (result > 0);
    }

    public async Task<bool> DeleteAsync(Assunto assunto)
    {
        var query = "DELETE FROM [dbo].[Assunto] WHERE [CodAs] = @CodAs";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, assunto);
        return (result > 0);
    }

    public async Task<Assunto> GetByCodeAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Assunto] WHERE [CodAs] = @Code";
        var result = await _dapperContext.DapperConnection.QueryFirstOrDefaultAsync<Assunto>(query, new { Code = code });
        return result;
    }
}
