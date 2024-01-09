using ProtonedMusicAPI.Interfaces.IFooter;

namespace ProtonedMusicAPI.Repositories
{
    public class FooterRepository : IFooterRepository
    {
        private readonly DatabaseContext _context;
        public FooterRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<FooterPost> CreateAsync(FooterPost newFooterPost)
        {
            _context.FooterPosts.Add(newFooterPost);
            await _context.SaveChangesAsync();
            return newFooterPost;
        }

        public async Task<FooterPost?> DeleteByIdAsync(int footerId)
        {
            var posts = await FindByIdAsync(footerId);

            if (posts != null) 
            {
                _context.Remove(posts);
                await _context.SaveChangesAsync();
            }
            return posts;
        }

        public async Task<FooterPost?> FindByIdAsync(int footerId)
        {
            return await _context.FooterPosts.FirstOrDefaultAsync(post => post.Id == footerId);
        }

        public async Task<List<FooterPost>> GetAllAsync()
        {
            return await _context.FooterPosts.ToListAsync();
        }

        public async Task<FooterPost?> UpdateByIdAsync(int footerId, FooterPost updateFooterPost)
        {
            FooterPost post = await FindByIdAsync(footerId);
            if (post != null)
            {
                post.Description = updateFooterPost.Description;
                post.Address = updateFooterPost.Address;
                post.AddressMapLink = updateFooterPost.AddressMapLink;
                post.Mail = updateFooterPost.Mail;
                post.Phonenumber = updateFooterPost.Phonenumber;

                await _context.SaveChangesAsync();

                post = await FindByIdAsync(post.Id);
            }
            return post;
        }
    }
}
