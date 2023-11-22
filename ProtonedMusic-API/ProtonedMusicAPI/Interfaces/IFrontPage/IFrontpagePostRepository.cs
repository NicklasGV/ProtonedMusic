namespace ProtonedMusicAPI.Interfaces.IFrontpage
{
    public interface IFrontpagePostRepository
    {
        Task<List<FrontpagePost>> GetAllAsync();
        Task<FrontpagePost?> FindByIdAsync(int frontpageId);
        Task<FrontpagePost> CreateAsync(FrontpagePost newFrontpage);
        Task<FrontpagePost?> UpdateByIdAsync(int frontpageId, FrontpagePost updateFrontpage);
        Task<FrontpagePost?> DeleteByIdAsync(int frontpageId);
        Task<FrontpagePost?> UploadFrontpagePicture(int frontpageId, IFormFile file);
    }
}
