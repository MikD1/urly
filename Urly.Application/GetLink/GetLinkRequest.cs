using MediatR;
using Urly.Domain;

namespace Urly.Application.GetLink
{
    public class GetLinkRequest : IRequest<Link>
    {
        public GetLinkRequest(string shortCode)
        {
            ShortCode = shortCode;
        }

        public string ShortCode { get; }
    }
}
