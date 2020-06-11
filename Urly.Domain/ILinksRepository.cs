using System.Threading.Tasks;

namespace Urly.Domain
{
    public interface ILinksRepository
    {
        Task<Link> GetLinkByIdAsync(int id);

        Task AddLinkAsync(Link link);
    }
}
