using Dapper;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Models;
using RR.Livraria.Infra.Contexts;

namespace RR.Livraria.Infra.Repositories;

public class AutorRepository : IAutorRepository
{
    private readonly DapperContext _dapperContext;
    public AutorRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<int> AddAsync(Autor autor)
    {
        var query = @"
            INSERT INTO [dbo].[Autor]
                ([Nome])
            OUTPUT
                Inserted.CodAu
            VALUES 
                (@Nome)";

        var result = await _dapperContext.DapperConnection.QuerySingleAsync<int>(query, autor);
        return result;
    }

    public async Task<IEnumerable<Autor>> GetAllAsync()
    {
        var query = "SELECT * FROM [dbo].[Autor]";
        var result = await _dapperContext.DapperConnection.QueryAsync<Autor>(query);
        return result;
    }

    public async Task<bool> UpdateAsync(Autor autor)
    {
        var query = @"
            UPDATE 
                [dbo].[Autor]
            SET
                [Nome] = @Nome
            WHERE
                [CodAu] = @CodAu";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, autor);
        return (result > 0);
    }

    public async Task<bool> DeleteAsync(Autor autor)
    {
        var query = "DELETE FROM [dbo].[Autor] WHERE [CodAu] = @CodAu";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, autor);
        return (result > 0);
    }

    public async Task<Autor> GetByCodeAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Autor] WHERE [CodAu] = @Code";
        var result = await _dapperContext.DapperConnection.QueryFirstOrDefaultAsync<Autor>(query, new { Code = code });
        return result;
    }
}
