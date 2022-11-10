using EasyShare.WebApi.Infrastructure.Database;
using EasyShare.WebApi.SharedContents.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.WebApi.SharedContents.GetSharedContent;

[ApiController]
[Route("shared-contents/{id:int}")]
[Tags("shared-contents")]
public class GetSharedContentController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public GetSharedContentController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet(Name = nameof(GetSharedContent))]
    [ProducesResponseType(typeof(SharedContent), StatusCodes.Status200OK)]
    public async Task<ActionResult<SharedContent>> GetSharedContent([FromRoute] int id)
    {
        var content = await _databaseContext.SharedContents.SingleOrDefaultAsync(sc => sc.Id == id)
                      ?? new SharedContent { Content = string.Empty };
        return Ok(content);
    }
}