using System;

namespace EasyShare.DbDeploy.Helper;

[Flags]
public enum ExitCode
{
    Success = 0,
    Error = 1,
    WrongParameters = 2,
}
