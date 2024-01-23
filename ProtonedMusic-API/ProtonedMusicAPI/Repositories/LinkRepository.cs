namespace ProtonedMusicAPI.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public LinkRepository(DatabaseContext databaseContext, IWebHostEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<Link>> GetAllAsync()
        {
            return await _databaseContext.Link
                .Include(l => l.Artist)
                .ThenInclude(a => a.Artist)
                .ToListAsync();
        }

        public async Task<Link> FindByIdAsync(int linkId)
        {
            return await _databaseContext.Link
                .Include(l => l.Artist)
                .ThenInclude(a => a.Artist)
                .FirstOrDefaultAsync(l => l.Id == linkId);
        }

        public async Task<Link> CreateAsync(Link newLink)
        {
            _databaseContext.Link.Add(newLink);
            await _databaseContext.SaveChangesAsync();
            return newLink;
        }

        public async Task<Link> DeleteByIdAsync(int linkId)
        {
            var link = await FindByIdAsync(linkId);

            if (link != null)
            {
                _databaseContext.Remove(link);
                await _databaseContext.SaveChangesAsync();
            }
            return link;
        }

        public async Task<Link> UpdateByIdAsync(int linkId, Link updateLink)
        {
            Link link = await FindByIdAsync(linkId);
            if (link != null)
            {
                link.Title = updateLink.Title;
                link.LinkAddress = updateLink.LinkAddress;

                await _databaseContext.SaveChangesAsync();

                link = await FindByIdAsync(link.Id);
            }
            return link;
        }
    }
}
