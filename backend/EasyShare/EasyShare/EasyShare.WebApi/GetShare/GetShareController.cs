using Microsoft.AspNetCore.Mvc;

namespace EasyShare.WebApi.GetShare;

[ApiController]
[Tags("shares")]
[Route("shares/{slug}")]
public class GetShareController : ControllerBase
{
    [HttpGet(Name = nameof(GetShare))]
    public ActionResult<string> GetShare([FromRoute] string slug)
    {
        return Ok(slug);
    }
}