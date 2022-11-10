namespace EasyShare.WebApi.Infrastructure.Database;

public interface IBaseEntity
{
    public DateTimeOffset Modified { get; }

    public DateTimeOffset Created { get; }
}