namespace ProtonedMusicAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DatabaseContext _context;

        public ImageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Image> Add(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<Image> DeleteImage(int id)
        {
            var pic = await _context.Images.FindAsync(id);

            if (pic is not null)
            {
                _context.Images.Remove(pic);
                await _context.SaveChangesAsync();
            }

            return pic;
        }

        public async Task<Image> GetImageById(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.ImageId == id);
        }
    }
}
