using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace EasyShare.DbDeploy.Helper;

public static class SqlScriptReader
{
    public static async Task<string> GetEmbeddedSqlFileAsync(string file)
    {
        var assembly = Assembly.GetAssembly(typeof(Program));
        await using var stream = assembly.GetManifestResourceStream(file);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}
