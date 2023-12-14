namespace ProtonedMusicAPI.Interfaces.IArtist
{
    public interface IArtistService
    {
        Task<List<ArtistResponse>> GetAllAsync();
        Task<ArtistResponse?> FindByIdAsync(int artistId);
        Task<ArtistResponse> CreateAsync(ArtistRequest newArtist);
        Task<ArtistResponse?> UpdateByIdAsync(int artistId, ArtistRequest updateArtist);
        Task<ArtistResponse> DeleteByIdAsync(int artistId);
        Task<ArtistResponse> UploadPicture(int artistId, IFormFile file);
    }
}
