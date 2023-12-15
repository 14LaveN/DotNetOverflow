using System.Data;
using Npgsql;

namespace DotNetOverflow.QuestionAPI.DAL.Database;

public class QuestionDbContext
{
    private readonly string? _connectionString = "Server=localhost;Port=5433;Database=DNOGeneralDbDevelopment;User Id=postgres;Password=1111;";

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}