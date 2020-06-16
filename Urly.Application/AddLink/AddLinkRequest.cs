using MediatR;
using Urly.Domain;

namespace Urly.Application.AddLink
{
    public class AddLinkRequest : IRequest<Link>
    {
        public AddLinkRequest(string? fullUrl)
        {
            FullUrl = fullUrl;
        }

        public string? FullUrl { get; }
    }
}
