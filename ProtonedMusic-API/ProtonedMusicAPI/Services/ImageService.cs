using ProtonedMusicAPI.DTO.ImageDTO;
using ProtonedMusicAPI.Database.Entities;

namespace ProtonedMusicAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        private static ImageResponse MapImageToImageResponse(Image image)
        {
            ImageResponse response = new ImageResponse
            {
                Id = image.Id,
                FileName = image.FileName,
                FilePath = image.FilePath,
            };
            return response;
        }
        private static Image MapImageRequestToImage(ImageRequest imageRequest)
        {
            return new Image
            {
                FileName = imageRequest.FileName,
            };
        }

        public Task<Image> CreateImage(Image createImage)
        {
            throw new NotImplementedException();
        }

        public Task<Image?> DeleteImageById(int ImageId)
        {
            throw new NotImplementedException();
        }

        public Task<Image?> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ImageResponse>> GetAll()
        {
            List<Image> images = await _imageRepository.GetAll();
            if (images == null)
            {
                throw new ArgumentNullException();
            }
            return images.Select(MapImageToImageResponse).ToList();
        }

        public Task<Image?> UpdateImage(Image updateImage)
        {
            throw new NotImplementedException();
        }

        public async Task<Image> UploadImage(ImageRequest imageRequest, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".PNG", ".GIF", ".JPG", ".JPEG", }; // Definer de tilladte filtyper
            var fileExtension = Path.GetExtension(imageFile.FileName);

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Invalid file type. Allowed file types: jpg, jpeg, png, gif");
            }

            // Gem billedet på serveren, f.eks. i en bestemt mappe
            var imagePath = "Uploads/" + imageRequest.FileName; // Ændr stien efter behov

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Opret et Image-objekt baseret på oplysningerne fra ImageRequest
            var newImage = new Image
            {
                FileName = imageRequest.FileName,
                FilePath = imagePath
            };

            // Gem Image-objektet i databasen
            await _imageRepository.CreateImage(newImage);

            return newImage;
        }


    }

}
