namespace ProtonedMusicAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DatabaseContext _context;

        public ImageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Image> Create(Image newImage)
        {
            _context.Images.Add(newImage);
            await _context.SaveChangesAsync();
            return newImage;
            
        }

        public async Task<Image?> DeleteById(int deleteImageId)
        {
            var image = await _context.Images.FindAsync(deleteImageId);

            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
                return image;
            }

            return null; // Billede blev ikke fundet
        }

        public async Task<Image?> FindById(Guid ImageId)
        {
            return await _context.Images.FirstOrDefaultAsync(image => image.ImageId == ImageId);
        }

        public Task<List<Category>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
