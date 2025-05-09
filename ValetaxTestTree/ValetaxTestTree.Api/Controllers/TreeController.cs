using MediatR;
using Microsoft.AspNetCore.Mvc;
using ValetaxTestTree.Api.Models;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Api.Controllers
{
    /// <summary>
    /// Represents entire tree API
    /// </summary>
    [Route("api/tree")]
    [ApiController]
    public class TreeController : BaseApiController
    {
        private readonly IMediator mediator;
        public TreeController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets or creates tree by name
        /// </summary>
        /// <remarks>
        /// Returns your entire tree. If your tree doesn't exist it will be created automatically.
        /// </remarks>
        /// <param name="query"></param>
        /// <returns>Entire tree</returns>
        /// <response code="200">Tree retrieved or created</response>
        /// <response code="500">Internal server or validation error occurred</response>
        [HttpPost]
        [ProducesResponseType(typeof(TreeResult), 200)]
        [ProducesResponseType(typeof(ErrorResult), 500)]
        public async Task<IActionResult> GetOrCreate([FromBody] GetOrCreateTreeCommand query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
