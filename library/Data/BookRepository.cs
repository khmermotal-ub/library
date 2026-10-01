using library.Models;
using Npgsql;

namespace library.Data;

// Every SQL statement about books lives in this one class.
public class BookRepository
{
    private readonly NpgsqlDataSource _dataSource;

    // ASP.NET Core hands in the data source that Program.cs registered.
    public BookRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    // How many books are there? One row, one column: a single number.
    public async Task<long> CountAsync()
    {
        const string sql = "SELECT COUNT(*) FROM lending.book;";

        // The command borrows a connection from the pool. 'await using' returns it
        // automatically when this method ends, even if an error happens.
        await using var command = _dataSource.CreateCommand(sql);

        // ExecuteScalarAsync returns the first column of the first row, as an object.
        object? result = await command.ExecuteScalarAsync();

        // COUNT(*) is a BIGINT in PostgreSQL, and a BIGINT is a long in C#.
        return (long)result!;
    }
}
