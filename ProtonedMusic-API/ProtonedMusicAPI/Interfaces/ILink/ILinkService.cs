namespace ProtonedMusicAPI.Interfaces.ILink
{
    public interface ILinkService
    {
        Task<List<LinkResponse>> GetAllAsync();
        Task<LinkResponse?> FindByIdAsync(int linkId);
        Task<LinkResponse> CreateAsync(LinkRequest newLink);
        Task<LinkResponse?> UpdateByIdAsync(int linkId, LinkRequest updateLink);
        Task<LinkResponse> DeleteByIdAsync(int linkId);
    }
}
