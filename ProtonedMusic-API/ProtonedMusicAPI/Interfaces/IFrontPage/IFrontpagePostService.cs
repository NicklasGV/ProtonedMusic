namespace ProtonedMusicAPI.Interfaces.IFrontpage
{
    public interface IFrontpagePostService
    {
        Task<List<FrontpagePostResponse>> GetAllAsync();
        Task<FrontpagePostResponse?> FindByIdAsync(int frontPageId);
        Task<FrontpagePostResponse> CreateAsync(FrontpagePostRequest newFrontPage);
        Task<FrontpagePostResponse?> UpdateByIdAsync(int frontPageId, FrontpagePostRequest updateFrontPage);
        Task<FrontpagePostResponse> DeleteByIdAsync(int frontPageId);
        Task<FrontpagePostResponse> UploadFrontpagePicture(int frontPageId, IFormFile file);
    }
}
