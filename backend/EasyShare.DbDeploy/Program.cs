using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DbUp;
using DbUp.Engine;
using EasyShare.DbDeploy.Helper;
using Microsoft.Extensions.Configuration;

namespace EasyShare.DbDeploy;

public class Program
{
    private const string SettingsFileDefaultFilename = "appsettings.json";
    private const string SettingsFileDevelopmentFilename = "appsettings.development.json";
    private const string AzureAccessToken = "AzureAccessToken";

    private const string CommandLineConnectionStringAlias = "claConnectionString";
    private const string CommandLineConnectionStringShortKey = "-connectionString";
    private const string ConnectionString = "DbConnectionString";

    public static async Task<int> Main(string[] args)
    {
        var switchMappings = new Dictionary<string, string>
        {
            { CommandLineConnectionStringShortKey, CommandLineConnectionStringAlias },
        };

        // Loads connection string settings from appsettings.json, environment variables and command line
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(SettingsFileDefaultFilename, false)
            .AddJsonFile(
                SettingsFileDevelopmentFilename,
                true) // This file is only needed locally for development and is individual per user and therefore not in source control.
            .AddEnvironmentVariables()
            .AddCommandLine(args, switchMappings)
            .Build();

        // Sets the connection string and token value from the command line or loaded from app settings
        var connectionString = configuration[CommandLineConnectionStringAlias] ?? configuration.GetConnectionString(ConnectionString);
        var accessToken = configuration[AzureAccessToken]?.TrimStart('"').TrimEnd('"');

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Usage();
            Console.WriteLine($"Error: {ExitCode.WrongParameters}");
            return (int)ExitCode.WrongParameters;
        }

        Console.WriteLine($"Connection string: {connectionString}");

        const bool createIfNotExists = true;
        var result = await CreateAndUpdateAsync(connectionString, accessToken, createIfNotExists);

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
#if DEBUG
            Console.ReadLine();
#endif
            return (int)ExitCode.Error;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return (int)ExitCode.Success;
    }

    private static async Task<DatabaseUpgradeResult> CreateAndUpdateAsync(string connectionString, string accessToken, bool createIfNotExists)
    {
        if (createIfNotExists)
        {
            await new Database(connectionString).CreateIfNotExistsAsync();
        }

        return DeployChanges.To
            .SqlDatabase(new AzureSqlConnectionManager(connectionString, accessToken))
            .WithScripts(new EmbeddedMigrationScriptsProvider())
            .WithTransaction()
            .WithExecutionTimeout(TimeSpan.FromMinutes(10))
            .LogToConsole()
            .LogScriptOutput()
            .Build()
            .PerformUpgrade();
    }

    private static void Usage()
    {
        Console.WriteLine("Usage: ");
        Console.WriteLine($"{Assembly.GetExecutingAssembly().GetName().Name} {CommandLineConnectionStringShortKey}=[ConnectionString] ");
        Console.WriteLine("Or provide an appsettings.json with an corresponding ConnectionStrings");
        Console.WriteLine("Examples:");
        Console.WriteLine(
            $"{Assembly.GetExecutingAssembly().GetName().Name} {CommandLineConnectionStringShortKey}=\"Data Source=[DBServer];Initial Catalog=[YourDatabase];Integrated Security=True;Persist Security Info=true;\"");
    }
}