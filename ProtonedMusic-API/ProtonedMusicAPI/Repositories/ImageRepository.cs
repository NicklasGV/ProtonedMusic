namespace ProtonedMusicAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DatabaseContext _context;

        public ImageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Image> CreateImage(Image createImage)
        {
            _context.Images.Add(createImage);

            await _context.SaveChangesAsync();
            return createImage;
        }

        public Task<Image?> DeleteImageById(int ImageId)
        {
            throw new NotImplementedException();
        }

        public Task<Image?> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Image>> GetAll()
        {
            return await _context.Images.ToListAsync();
        }

        public Task<Image?> UpdateImage(Image updateImage)
        {
            throw new NotImplementedException();
        }
    }
}
