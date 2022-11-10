using EasyShare.WebApi.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.WebApi.SharedContents.GetSharedContents;

[ApiController]
[Route("shared-contents")]
[Tags("shared-contents")]
public class GetSharesController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public GetSharesController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet(Name = nameof(GetSharedContents))]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<string>>> GetSharedContents()
    {
        var sharedContents = await _databaseContext.SharedContents.ToListAsync();
        return Ok(sharedContents);
    }
}