using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DbUp;

namespace EasyShare.DbDeploy.Helper;

public class Database
{
    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CreateIfNotExistsAsync()
    {
        if (await ExistsAsync(_connectionString))
        {
            return;
        }

        Console.WriteLine("Create and configure database...");
        EnsureDatabase.For.SqlDatabase(_connectionString);
        await ConfigureAsync(_connectionString);
    }

    private static async Task<bool> ExistsAsync(string connectionString)
    {
        var databaseName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;
        var masterConnectionString = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master" }.ConnectionString;
        await using var connection = new SqlConnection(masterConnectionString);
        await using var command = new SqlCommand($"SELECT 1 FROM sys.databases AS DB WHERE DB.name = '{databaseName}'", connection);
        await command.Connection.OpenAsync();
        var scalar = (int?)await command.ExecuteScalarAsync();

        return scalar.HasValue;
    }

    private static async Task ConfigureAsync(string connectionString)
    {
        await using var connection = new SqlConnection(connectionString);
        var script = await SqlScriptReader.GetEmbeddedSqlFileAsync("EasyShare.DbDeploy.Helper.InitialDatabaseSetup.sql");
        await using var command = new SqlCommand(script.Replace("<DatabaseName>", connection.Database), connection);
        await command.Connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}
