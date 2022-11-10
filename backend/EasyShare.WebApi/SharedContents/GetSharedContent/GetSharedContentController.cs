using EasyShare.WebApi.Infrastructure.Database;
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
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSharedContent([FromRoute] int id)
    {
        var content = await _databaseContext.SharedContents.SingleOrDefaultAsync(sc => sc.Id == id);
        return Ok(content);
    }
}