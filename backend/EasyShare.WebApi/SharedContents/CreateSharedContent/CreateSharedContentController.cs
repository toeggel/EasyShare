using Microsoft.AspNetCore.Mvc;

namespace EasyShare.WebApi.SharedContents.CreateSharedContent;

[ApiController]
[Route("shared-contents")]
[Tags("shared-contents")]
public class CreateSharedContentController : ControllerBase
{
    [HttpPost(Name = nameof(CreateSharedContent))]
    public IActionResult CreateSharedContent()
    {
        // todo: store to DB
        return Ok();
    }
}