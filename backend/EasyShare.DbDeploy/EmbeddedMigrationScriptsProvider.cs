using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DbUp.Engine;
using DbUp.Engine.Transactions;
using EasyShare.DbDeploy.Helper;

namespace EasyShare.DbDeploy;

public class EmbeddedMigrationScriptsProvider : IScriptProvider
{
    private static readonly string ScriptsFolderDot = $"{typeof(Program).Namespace}.Scripts.";

    public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
    {
        var sqlScripts = new List<SqlScript>();

        foreach (var (resourceName, fileName) in Assembly
                     .GetAssembly(typeof(EmbeddedMigrationScriptsProvider))
                     .GetManifestResourceNames()
                     .Where(s => s.EndsWith(".sql"))
                     .Where(s => s.StartsWith(ScriptsFolderDot))
                     .Select(GetFileName)
                     .OrderBy(s => s.FileName))
        {
            sqlScripts.Add(
                new SqlScript(
                    fileName,
                    SqlScriptReader.GetEmbeddedSqlFileAsync(resourceName)
                        .GetAwaiter()
                        .GetResult()));
        }

        return sqlScripts;
    }

    private static (string ResourceName, string FileName) GetFileName(string resourceName)
    {
        var stringParts = resourceName.Split(".");
        var fileName = string.Concat(stringParts.SkipLast(1).Last(), ".", stringParts.Last());
        return (resourceName, fileName);
    }
}