using MediatR;
using Microsoft.AspNetCore.Mvc;
using ValetaxTestTree.Api.Models;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Api.Controllers
{
    /// <summary>
    /// Represents tree node API
    /// </summary>
    [Route("api/tree/node")]
    [ApiController]
    public class TreeNodeController : BaseApiController
    {
        private readonly IMediator mediator;

        public TreeNodeController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Creates node
        /// </summary>
        /// <remarks>
        /// Creates a new node in your tree. You must to specify a parent node ID that belongs to your tree.
        /// A new node name must be unique across all siblings.
        /// </remarks>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="201">Node created</response>
        /// <response code="500">Internal server or validation error occurred</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErrorResult), 500)]
        public async Task<IActionResult> Create([FromBody] CreateTreeNodeCommand query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            await mediator.Send(query);

            return Created();
        }

        /// <summary>
        /// Deletes node
        /// </summary>
        /// <remarks>
        /// Deletes an existing node in your tree. You must specify a node ID that belongs your tree.
        /// </remarks>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="204">Node deleted</response>
        /// <response code="500">Internal server or validation error occurred</response>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorResult), 500)]
        public async Task<IActionResult> Delete([FromBody] DeleteTreeNodeCommand query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            await mediator.Send(query);

            return NoContent();
        }

        /// <summary>
        /// Renames node
        /// </summary>
        /// <remarks>
        /// Renames an existing node in your tree. You must specify a node ID that belongs your tree.
        /// A new name of the node must be unique across all siblings.
        /// </remarks>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="204">Node renamed</response>
        /// <response code="500">Internal server or validation error occurred</response>
        [HttpPut("rename")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorResult), 500)]
        public async Task<IActionResult> Rename([FromBody] RenameTreeNodeCommand query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            await mediator.Send(query);

            return NoContent();
        }
    }
}
