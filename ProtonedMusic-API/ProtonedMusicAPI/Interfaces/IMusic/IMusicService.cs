using ProtonedMusicAPI.DTO.MusicDTO;

namespace ProtonedMusicAPI.Interfaces.IMusic
{
    public interface IMusicService
    {
        Task<List<MusicResponse>> GetAllAsync();
        Task<MusicResponse?> FindByIdAsync(int musicId);
        Task<MusicResponse> CreateAsync(MusicRequest newMusic);
        Task<MusicResponse?> UpdateByIdAsync(int musicId, MusicRequest updateMusic);
        Task<MusicResponse> DeleteByIdAsync(int musicId);
        Task<MusicResponse> UploadSong(int musicId, IFormFile song);
        Task<MusicResponse> UploadSongPicture(int musicId, IFormFile file);
    }
}
