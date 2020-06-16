using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Urly.Application.GetLink;
using Urly.Domain;

namespace Urly.WebApi.Controllers
{
    [Route("/")]
    public class RedirectController : ControllerBase
    {
        public RedirectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{code}")]
        public async Task<ActionResult> RedirectFullUrl(string code)
        {
            var request = new GetLinkRequest(code);
            Link link = await _mediator.Send(request);

            // _logger.LogInformation($"Redirect: {link.ShortCode} -> {link.FullUrl}");
            return Redirect(link.FullUrl);
        }

        private readonly IMediator _mediator;
    }
}
