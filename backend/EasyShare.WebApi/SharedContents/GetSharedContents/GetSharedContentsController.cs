using Microsoft.AspNetCore.Mvc;

namespace EasyShare.WebApi.SharedContents.GetSharedContents;

[ApiController]
[Route("shared-contents")]
[Tags("shared-contents")]
public class GetSharesController : ControllerBase
{
    [HttpGet(Name = nameof(GetSharedContents))]
    public IEnumerable<string> GetSharedContents()
    {
        return new List<string>
        {
            "asd",
            "123123",
            "ds fsd f",
        };
    }
}