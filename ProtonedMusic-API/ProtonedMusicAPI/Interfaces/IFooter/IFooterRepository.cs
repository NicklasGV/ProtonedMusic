namespace ProtonedMusicAPI.Interfaces.IFooter
{
    public interface IFooterRepository
    {
        Task<List<FooterPost>> GetAllAsync();
        Task<FooterPost?> FindByIdAsync(int footerId);
        Task<FooterPost> CreateAsync(FooterPost newFooterPost);
        Task<FooterPost?> UpdateByIdAsync(int footerId, FooterPost updateFooterPost);
        Task<FooterPost?> DeleteByIdAsync(int footerId);
    }
}
