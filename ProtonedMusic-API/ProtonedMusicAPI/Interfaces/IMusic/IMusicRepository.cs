namespace ProtonedMusicAPI.Interfaces.IMusic
{
    public interface IMusicRepository
    {
        Task<List<Music>> GetAllAsync();
        Task<Music?> FindByIdAsync(int musicId);
        Task<Music> CreateAsync(Music newMusic);
        Task<Music?> UpdateByIdAsync(int musicId, Music updateMusic);
        Task<Music?> DeleteByIdAsync(int musicId);
        Task<Music?> UploadSong(int musicId, IFormFile song);
        Task<Music?> UploadSongPicture(int musicId, IFormFile file);
    }
}
