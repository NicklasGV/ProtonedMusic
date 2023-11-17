using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.DTO.MusicDTO;

namespace ProtonedMusicAPI.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;


        public MusicService(IMusicRepository musicRepository)
        {
            _musicRepository = musicRepository;
        }

        public static MusicResponse MapMusicToMusicResponse(Music music)
        {
            MusicResponse response = new MusicResponse
            {
                Id = music.Id,
                SongName = music.SongName,
                Artist = music.Artist,
                Album = music.Album,
                SongFilePath = music.SongFilePath,
                SongPicturePath = music.SongPicturePath,
            };
            return response;
        }

        private static Music MapMusicRequestToMusic(MusicRequest musicRequest)
        {
            Music music = new Music
            {
                SongName = musicRequest.SongName,
                Artist = musicRequest.Artist,
                Album = musicRequest.Album,
                SongFilePath = musicRequest.SongFilePath ?? string.Empty,
                SongPicturePath = musicRequest.SongPicturePath ?? string.Empty,
            };
            return music;
        }

        public async Task<List<MusicResponse>> GetAllAsync()
        {
            List<Music> musics = await _musicRepository.GetAllAsync();

            if (musics == null)
            {
                throw new ArgumentException();
            }
            return musics.Select(MapMusicToMusicResponse).ToList();
        }

        public async Task<MusicResponse> FindByIdAsync(int musicId)
        {
            var music = await _musicRepository.FindByIdAsync(musicId);

            if (music != null)
            {
                return MapMusicToMusicResponse(music);
            }

            return null;
        }

        public async Task<MusicResponse> CreateAsync(MusicRequest newMusic)
        {
            var music = await _musicRepository.CreateAsync(MapMusicRequestToMusic(newMusic));
            if (music == null)
            {
                throw new ArgumentNullException();
            }
            return MapMusicToMusicResponse(music);
        }

        public async Task<MusicResponse> DeleteByIdAsync(int musicId)
        {
            var music = await _musicRepository.DeleteByIdAsync(musicId);

            if (music != null)
            {
                return MapMusicToMusicResponse(music);
            }
            return null;
        }

        public async Task<MusicResponse> UpdateByIdAsync(int musicId, MusicRequest updateMusic)
        {
            var music = MapMusicRequestToMusic(updateMusic);
            var insertedMusic = await _musicRepository.UpdateByIdAsync(musicId, music);

            if (insertedMusic != null)
            {
                return MapMusicToMusicResponse(insertedMusic);
            }

            return null;
        }

        public async Task<MusicResponse> UploadSong(int musicId, IFormFile song)
        {
            Music music = await _musicRepository.UploadSong(musicId, song);

            if (music != null)
            {
                return MapMusicToMusicResponse(music);
            }

            return null;

        }

        public async Task<MusicResponse> UploadSongPicture(int musicId, IFormFile file)
        {
            Music music = await _musicRepository.UploadSongPicture(musicId, file);

            if (music != null)
            {
                return MapMusicToMusicResponse(music);
            }

            return null;

        }
    }
}
