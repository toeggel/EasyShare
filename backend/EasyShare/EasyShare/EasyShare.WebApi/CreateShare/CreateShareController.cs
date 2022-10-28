using Microsoft.AspNetCore.Mvc;

namespace EasyShare.WebApi.CreateShare;

[ApiController]
[Tags("shares")]
[Route("shares")]
public class CreateShareController : ControllerBase
{
    [HttpPost(Name = nameof(CreateShare))]
    public IActionResult CreateShare()
    {
        // todo: store to DB
        return Ok();
    }
}