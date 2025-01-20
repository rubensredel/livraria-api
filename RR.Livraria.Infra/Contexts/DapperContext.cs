using System.Data.Common;

namespace RR.Livraria.Infra.Contexts;

public class DapperContext
{
    private readonly DbConnection _connection;

    public DbConnection DapperConnection { get => _connection; }

    public DapperContext(DbConnection connection)
    {
        _connection = connection;
    }
}
