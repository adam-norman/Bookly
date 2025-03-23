using Application.Features.Apartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Apartments
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentsController : ControllerBase
    {
        private readonly ISender _sender;
        public ApartmentsController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchApartments(DateOnly StartDate, DateOnly EndDate, CancellationToken cancellationToken)
        {
            var searchQuery = new SearchApartmentsQuery(StartDate, EndDate);
            var result = await _sender.Send(searchQuery, cancellationToken);
            return Ok(result.Value);
        }
    }
}
