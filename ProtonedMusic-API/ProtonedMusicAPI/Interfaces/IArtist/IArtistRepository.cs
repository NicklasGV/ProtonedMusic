namespace ProtonedMusicAPI.Interfaces.IArtist
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();
        Task<Artist?> FindByIdAsync(int artistId);
        Task<Artist> CreateAsync(Artist newArtist);
        Task<Artist?> UpdateByIdAsync(int artistId, Artist updateArtist);
        Task<Artist?> DeleteByIdAsync(int artistId);
        Task<Artist?> UploadPicture(int artistId, IFormFile file);
    }
}
