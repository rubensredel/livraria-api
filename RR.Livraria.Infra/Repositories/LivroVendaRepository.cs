using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Models;
using RR.Livraria.Infra.Contexts;

namespace RR.Livraria.Infra.Repositories;

public class LivroVendaRepository : ILivroVendaRepository
{
    private readonly DapperContext _dapperContext;

    public LivroVendaRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<bool> AddAsync(LivroVenda model)
    {
        var query = @"
            INSERT INTO [dbo].[Livro_Venda]
                ([Livro_Codl], [Venda_CodV], [Valor])
            VALUES 
                (@Livro_Codl, @Venda_CodV, @Valor)";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, model);
        return result > 0;
    }

    public async Task<IEnumerable<LivroVenda>> GetByCodlAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Livro_Venda] WHERE Livro_Codl = @code";
        var result = await _dapperContext.DapperConnection.QueryAsync<LivroVenda>(query, new { code });
        return result;
    }

    public async Task<IEnumerable<LivroVenda>> GetByCodVAsync(int code)
    {
        var query = "SELECT * FROM [dbo].[Livro_Venda] WHERE Venda_CodV = @code";
        var result = await _dapperContext.DapperConnection.QueryAsync<LivroVenda>(query, new { code });
        return result;
    }

    public async Task<bool> DeleteAsync(LivroVenda model)
    {
        var query = @"
            DELETE FROM 
                [dbo].[Livro_Venda]
            WHERE 
                [Livro_Codl] = @Livro_Codl AND
                [Venda_CodV] = @Venda_CodV";
        var result = await _dapperContext.DapperConnection.ExecuteAsync(query, model);
        return (result > 0);
    }
}
