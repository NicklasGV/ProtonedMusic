using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ImageService(IImageRepository imageRepository, IWebHostEnvironment hostingEnvironment)
    {
        _imageRepository = imageRepository;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<ImageResponse> Create(ImageRequest newImage)
    {
        // 1. Validering af det uploadede billede
        if (newImage.ImageFile == null || newImage.ImageFile.Length == 0)
        {
            throw new ArgumentException("Invalid image file.");
        }

        // 2. Generer et unikt filnavn for billedet
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + newImage.ImageFile.FileName;

        // 3. Gem billedet på serveren
        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
        string imagePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await newImage.ImageFile.CopyToAsync(stream);
        }

        // 4. Opret en databasepost med stien til det gemte billede
        Image imageEntity = new Image
        {
            ImageId = Guid.NewGuid(), // Generer en ny Guid
            Name = newImage.Name,
            ImagePath = imagePath
        };

        await _imageRepository.Create(imageEntity);

        var imageResponse = new ImageResponse
        {
            ImageId = imageEntity.ImageId,
            Name = imageEntity.Name,
            ImagePath = imageEntity.ImagePath
        };

        return imageResponse;
    }


    public Task<ImageResponse?> DeleteById(int ImageId)
    {
        throw new NotImplementedException();
    }

    public async Task<ImageResponse?> FindById(Guid ImageId)
    {
        // Søg efter billede i databasen baseret på ImageId
        var imageEntity = await _imageRepository.FindById(ImageId);

        if (imageEntity == null)
        {
            return null; // Billede blev ikke fundet
        }

        // Konverter databaseentiteten til en ImageResponse
        var imageResponse = new ImageResponse
        {
            ImageId = imageEntity.ImageId,
            Name = imageEntity.Name,
            ImagePath = imageEntity.ImagePath
        };

        return imageResponse;
    }

    public Task<List<ImageResponse>> GetAll()
    {
        throw new NotImplementedException();
    }
}
