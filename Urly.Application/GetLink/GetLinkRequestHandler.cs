using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Urly.Domain;
using Urly.Domain.Exceptions;

namespace Urly.Application.GetLink
{
    public class GetLinkRequestHandler : IRequestHandler<GetLinkRequest, Link>
    {
        public GetLinkRequestHandler(ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }

        public async Task<Link> Handle(GetLinkRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ShortCode))
            {
                throw new InvalidOperationDomainException("Invalid Short code.");
            }

            var encoder = new ShortCodeEncoder();
            int id = encoder.Decode(request.ShortCode);
            Link? link = await _linksRepository.GetLinkByIdAsync(id);
            if (link is null)
            {
                throw new NotFoundDomainException($"Link '{request.ShortCode}' not found.");
            }

            return link;
        }

        private readonly ILinksRepository _linksRepository;
    }
}
