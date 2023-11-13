namespace ProtonedMusicAPI.Interfaces.IFrontPage
{
    public interface IFrontPagePostRepository
    {
        Task<List<FrontpagePost>> GetAllAsync();
        Task<FrontpagePost?> FindByIdAsync(int frontpageId);
        Task<FrontpagePost> CreateAsync(FrontpagePost newFrontpage);
        Task<FrontpagePost?> UpdateByIdAsync(int frontpageId, FrontpagePost updateFrontpage);
        Task<FrontpagePost?> DeleteByIdAsync(int frontpageId);
        Task<FrontpagePost?> UploadUploadFrontpagePicturePicture(int frontpageId, IFormFile file);
    }
}
