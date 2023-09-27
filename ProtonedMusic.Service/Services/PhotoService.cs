using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ProtonedMusic.Service.Services
{
    //public class PhotoService : IPhotoService
    //{
    //    public readonly Cloudinary cloudinary;

    //    public PhotoService(IConfiguration config)
    //    {
    //        Account account = new Account(
    //            config.GetSection("CloudinarySettings:CloudName").Value,
    //            config.GetSection("CloudinarySettings:ApiKey").Value,
    //            config.GetSection("CloudinarySettings:ApiSecret").Value);

    //        cloudinary = new Cloudinary(account);
    //    }


    //    public async Task<DeletionResult> DeletePhotoAsync(string photoId)
    //    {
    //        var deleteParams = new DeletionParams(photoId);

    //        var result = await cloudinary.DestroyAsync(deleteParams);

    //        return result;
    //    }

    //    public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo)
    //    {
    //        var uploadResult = new ImageUploadResult();
    //        if (photo.Length > 0 && photo.Length < 10000000)
    //        {
    //            using var stream = photo.OpenReadStream();
    //            var uploadParams = new AutoUploadParams
    //            {
    //                File = new FileDescription(photo.FileName, stream)
    //            };
    //            uploadResult = await cloudinary.UploadAsync(uploadParams);
    //        }
    //        else
    //        {
    //            uploadResult = new ImageUploadResult()
    //            {
    //                Error = new Error(),
    //            };
    //        }

    //        return uploadResult;
    //    }
    //}
}
