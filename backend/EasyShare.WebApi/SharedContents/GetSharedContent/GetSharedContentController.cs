using Microsoft.AspNetCore.Mvc;

namespace EasyShare.WebApi.SharedContents.GetSharedContent;

[ApiController]
[Route("shared-contents/{id}")]
[Tags("shared-contents")]
public class GetSharedContentController : ControllerBase
{
    [HttpGet(Name = nameof(GetSharedContent))]
    public ActionResult<string> GetSharedContent([FromRoute] string id)
    {
        return Ok(id);
    }
}