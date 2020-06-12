using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Urly.Domain;
using Urly.Dto;

namespace Urly.WebApi.Controllers
{
    // TODO: Use Mediator
    [Route("api/v1/links")]
    public class LinksController : ControllerBase
    {
        public LinksController(IMapper mapper, ILinksRepository linksRepository)
        {
            _mapper = mapper;
            _linksRepository = linksRepository;
        }

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LinkDto>> GetLink(string code)
        {
            var encoder = new ShortCodeEncoder();
            int id = encoder.Decode(code);
            Link link = await _linksRepository.GetLinkByIdAsync(id);

            var linkDto = _mapper.Map<LinkDto>(link);
            linkDto.ShortCode = code;
            return Ok(linkDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LinkDto>> PostLink([FromBody] CreateLinkDto createLinkDto)
        {
            var link = new Link(createLinkDto.FullUrl);
            await _linksRepository.AddLinkAsync(link);

            var linkDto = _mapper.Map<LinkDto>(link);
            var encoder = new ShortCodeEncoder();
            linkDto.ShortCode = encoder.Encode(link.Id);
            return CreatedAtAction(nameof(GetLink), new { code = linkDto.ShortCode }, linkDto);
        }

        private readonly IMapper _mapper;
        private readonly ILinksRepository _linksRepository;
    }
}
