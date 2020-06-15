using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Urly.Domain;
using Urly.Domain.Exceptions;

namespace Urly.Application.AddLink
{
    public class AddLinkRequestHandler : IRequestHandler<AddLinkRequest, Link>
    {
        public AddLinkRequestHandler(ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }

        public async Task<Link> Handle(AddLinkRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.FullUrl))
            {
                throw new InvalidOperationDomainException("Invalid Full URL.");
            }

            var link = new Link(request.FullUrl);
            await _linksRepository.AddLinkAsync(link);
            return link;
        }

        private readonly ILinksRepository _linksRepository;
    }
}
