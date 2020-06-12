using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Urly.Domain;
using Urly.Domain.Exceptions;

namespace Urly.Application
{
    public class LinksRepository : ILinksRepository
    {
        public LinksRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Link> GetLinkByIdAsync(int id)
        {
            Link link = await _dbContext.Links.FirstOrDefaultAsync(x => x.Id == id);
            if (link is null)
            {
                throw new NotFoundDomainException($"Link not found.");
            }

            return link;
        }

        public async Task AddLinkAsync(Link link)
        {
            if (link is null)
            {
                throw new InvalidOperationDomainException("Invalid Link.");
            }

            await _dbContext.Links.AddAsync(link);
            await _dbContext.SaveChangesAsync();
        }

        private readonly AppDbContext _dbContext;
    }
}
