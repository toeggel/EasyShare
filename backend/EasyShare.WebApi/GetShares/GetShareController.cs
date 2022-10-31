using Microsoft.AspNetCore.Mvc;

namespace EasyShare.WebApi.GetShares;

[ApiController]
[Tags("shares")]
[Route("shares")]
public class GetSharesController : ControllerBase
{
    [HttpGet(Name = nameof(GetShares))]
    public IEnumerable<string> GetShares()
    {
        return new List<string>
        {
            "asd",
            "123123",
            "ds fsd f",
        };
    }
}