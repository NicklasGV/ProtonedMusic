using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ImageService(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task<Image> GetImageById(int id)
    {
        return await _imageRepository.GetImageById(id);
    }

    public async Task<Image> AddImage(Image picture)
    {
        var pic = await _imageRepository.Add(picture);
        return pic;
    }

    public async Task<Image> DeleteImage(int id)
    {
        var pic = await _imageRepository.DeleteImage(id);

        return pic;
    }

}
