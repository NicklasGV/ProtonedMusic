namespace ProtonedMusicAPI.Interfaces.ILink
{
    public interface ILinkRepository
    {
        Task<List<Link>> GetAllAsync();
        Task<Link?> FindByIdAsync(int linkId);
        Task<Link> CreateAsync(Link newLink);
        Task<Link?> UpdateByIdAsync(int linkId, Link updateLink);
        Task<Link?> DeleteByIdAsync(int linkId);
    }
}
