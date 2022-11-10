using EasyShare.WebApi.Infrastructure.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShare.WebApi.SharedContents.Entities;

[Table(nameof(SharedContent))]
public class SharedContent: IBaseEntity
{
    [Key]
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTimeOffset Modified { get; set; }

    public DateTimeOffset Created { get; set; }
}