using EasyShare.WebApi.Infrastructure.Database;
using EasyShare.WebApi.SharedContents.Entities;
using EasyShare.WebApi.SharedContents.GetSharedContent;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.WebApi.SharedContents.CreateSharedContent;

[ApiController]
[Route("shared-contents")]
[Tags("shared-contents")]
public class CreateSharedContentController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public CreateSharedContentController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpPost(Name = nameof(CreateSharedContent))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<CreatedAtRouteResult> CreateSharedContent([FromBody] string content)
    {
        var sharedContent = new SharedContent { Content = content };
        await _databaseContext.SharedContents.AddAsync(sharedContent);
        await _databaseContext.SaveChangesAsync();
        const string actionName = nameof(GetSharedContentController.GetSharedContent);

        return CreatedAtRoute(actionName, new { id = sharedContent.Id }, sharedContent);
    }
}