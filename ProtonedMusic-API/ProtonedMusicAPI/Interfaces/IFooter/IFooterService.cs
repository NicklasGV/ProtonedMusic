using ProtonedMusicAPI.DTO.FooterPostDTO;

namespace ProtonedMusicAPI.Interfaces.IFooter
{
    public interface IFooterService
    {
        Task<List<FooterResponse>> GetAllAsync();
        Task<FooterResponse?> FindByIdAsync(int footerId);
        Task<FooterResponse> CreateAsync(FooterRequest newFooterPost);
        Task<FooterResponse?> UpdateByIdAsync(int footerId, FooterRequest updateFooterPost);
        Task<FooterResponse> DeleteByIdAsync(int footerId);
    }
}
