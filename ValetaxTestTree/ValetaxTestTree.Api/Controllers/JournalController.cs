using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ValetaxTestTree.Api.Models;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Api.Controllers
{
    /// <summary>
    /// Represents journal API
    /// </summary>
    [Route("api/journal")]
    [ApiController]
    public class JournalController : BaseApiController
    {
        private readonly IMediator mediator;

        public JournalController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets journal events by filter
        /// </summary>
        /// <remarks>
        /// Provides the pagination API. Skip means the number of items should be skipped by server.
        /// Take means the maximum number items should be returned by server. All fields of the filter are optional.
        /// </remarks>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="200">Range of journal events retrieved</response>
        /// <response code="500">Internal server or validation error occurred</response>
        [HttpPost("getRange")]
        [ProducesResponseType(typeof(RangeResult<JournalItemResult>), 200)]
        [ProducesResponseType(typeof(ErrorResult), 500)]
        public async Task<IActionResult> GetRange([FromBody] GetJournalEventRangeQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var result = await mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Gets journal event by id
        /// </summary>
        /// <remarks>
        /// Returns the information about an particular event by ID.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Journal event retrieved</response>
        /// <response code="500">Internal server or validation error occurred</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(JournalEventResult), 200)]
        [ProducesResponseType(typeof(ErrorResult), 500)]
        public async Task<IActionResult> Get([Required] int id)
        {
            var query = new GetJournalEventQuery { Id = id };
            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
