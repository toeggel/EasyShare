using EasyShare.WebApi.Infrastructure.Database;
using EasyShare.WebApi.SharedContents.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.WebApi.SharedContents.GetSharedContents;

[ApiController]
[Route("shared-contents")]
[Tags("shared-contents")]
public class GetSharedContentsController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public GetSharedContentsController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet(Name = nameof(GetSharedContents))]
    [ProducesResponseType(typeof(IEnumerable<SharedContent>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SharedContent>>> GetSharedContents()
    {
        var sharedContents = await _databaseContext.SharedContents.ToListAsync();
        return Ok(sharedContents);
    }
}