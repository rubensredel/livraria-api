using Dapper;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Models;
using RR.Livraria.Infra.Contexts;

namespace RR.Livraria.Infra.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly DapperContext _dapperContext;
    public VendaRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<int> AddAsync(Venda venda)
    {
        var query = @"
            INSERT INTO [dbo].[Venda]
                ([Descricao])
            OUTPUT
                Inserted.CodV
            VALUES 
                (@Descricao)";
        var result = await _dapperContext.DapperConnection.QuerySingleAsync<int>(query, venda);
        return result;
    }

    public async Task<IEnumerable<Venda>> GetAllAsync()
    {
        var query = "SELECT * FROM [dbo].[Venda]";
        var result = await _dapperContext.DapperConnection.QueryAsync<Venda>(query);
        return result;
    }

    public async Task<bool> UpdateAsync(Venda venda)
    {
        var query = @"
            UPDATE 
                [dbo].[Venda]
            SET
                [Descricao] = @Descricao
            WHERE
                [CodV] = @CodV";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, venda);
        return (result > 0);
    }

    public async Task<bool> DeleteAsync(Venda venda)
    {
        var query = "DELETE FROM [dbo].[Venda] WHERE [CodV] = @CodV";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, venda);
        return (result > 0);
    }

    public async Task<Venda> GetByCodeAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Venda] WHERE [CodV] = @Code";
        var result = await _dapperContext.DapperConnection.QueryFirstOrDefaultAsync<Venda>(query, new { Code = code });
        return result;
    }
}
