using System.Collections.Generic;
using System.Data.SqlClient;
using DbUp.Engine.Transactions;
using DbUp.Support;

namespace EasyShare.DbDeploy.Helper;

/// <summary>
/// This class is used to define a ConnectionManager which allows to pass the AccessToken from outside.
/// The built-in ConnectionManager only supports to generate the AccessToken itself.
/// </summary>
internal class AzureSqlConnectionManager : DatabaseConnectionManager
{
    public AzureSqlConnectionManager(string connectionString, string accessToken)
        : base(new DelegateConnectionFactory((log, dbManager) =>
        {
            var sqlConnection = new SqlConnection(connectionString);
            if (!string.IsNullOrEmpty(accessToken))
            {
                sqlConnection.AccessToken = accessToken;
            }

            if (dbManager.IsScriptOutputLogged)
            {
                sqlConnection.InfoMessage += (_, e) => log.WriteInformation("{0}", e.Message);
            }

            return sqlConnection;
        }))
    {
    }

    public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents) => new SqlCommandSplitter().SplitScriptIntoCommands(scriptContents);
}
