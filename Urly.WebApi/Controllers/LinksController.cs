using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Urly.Application.AddLink;
using Urly.Application.GetLink;
using Urly.Domain;
using Urly.Dto;

namespace Urly.WebApi.Controllers
{
    [Route("api/v1/links")]
    public class LinksController : ControllerBase
    {
        public LinksController(IMapper mapper, IMediator mediator, ILinksRepository linksRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LinkDto>> GetLink(string code)
        {
            var request = new GetLinkRequest(code);
            Link link = await _mediator.Send(request);
            var linkDto = _mapper.Map<LinkDto>(link);
            linkDto.ShortCode = code;
            return Ok(linkDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LinkDto>> PostLink([FromBody] CreateLinkDto createLinkDto)
        {
            var request = new AddLinkRequest(createLinkDto?.FullUrl);
            Link link = await _mediator.Send(request);

            var linkDto = _mapper.Map<LinkDto>(link);
            return CreatedAtAction(nameof(GetLink), new { code = linkDto.ShortCode }, linkDto);
        }

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
    }
}
