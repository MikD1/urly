using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Urly.Domain;

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
            Link link = await _dbContext.Links.FirstAsync(x => x.Id == id);
            return link;
        }

        public async Task AddLinkAsync(Link link)
        {
            await _dbContext.Links.AddAsync(link);
            await _dbContext.SaveChangesAsync();
        }

        private readonly AppDbContext _dbContext;
    }
}
