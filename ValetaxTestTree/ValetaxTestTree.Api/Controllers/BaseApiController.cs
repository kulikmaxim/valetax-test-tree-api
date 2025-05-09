using Microsoft.AspNetCore.Mvc;

namespace ValetaxTestTree.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseApiController : ControllerBase
    {
        [NonAction]
        public CreatedResult Created()
        {
            return new CreatedResult(string.Empty, default);
        }
    }
}
