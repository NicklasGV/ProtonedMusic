using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.DTO.EmailDTO;
using ProtonedMusicAPI.DTO.ProductDTO;

namespace ProtonedMusicAPI.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;


        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public static ArtistResponse MapArtistToArtistResponse(Artist artist)
        {
            ArtistResponse response = new ArtistResponse
            {
                Id = artist.Id,
                Name = artist.Name,
                Info = artist.Info,
                PicturePath = artist.PicturePath,
                

            };
            if (artist.User != null)
            {
                response.User = new ArtistUserResponse
                {
                    Id = artist.User.Id,
                    FirstName = artist.User.FirstName,
                    LastName = artist.User.LastName,
                    Email = artist.User.Email.ToLower(),
                    Role = artist.User.Role,
                    PhoneNumber = artist.User.PhoneNumber,
                    Address = artist.User.Address,
                    Country = artist.User.Country,
                    City = artist.User.City,
                    Postal = artist.User.Postal
                };
            }
            if (artist.Songs.Count > 0)
            {
                response.Songs = artist.Songs.Select(x => new ArtistSongResponse
                {
                    Id = x.Music.Id,
                    SongName = x.Music.SongName,
                    Album = x.Music.Album,
                    SongPicturePath = x.Music.SongPicturePath,
                    SongFilePath = x.Music.SongFilePath
                }).ToList();
            }
            if (artist.Links.Count > 0)
            {
                response.Links = artist.Links.Select(x => new ArtistLinkResponse
                {
                    Id = x.Link.Id,
                    Title = x.Link.Title,
                    LinkAddress = x.Link.LinkAddress
                }).ToList();
            }
            return response;
        }

        private static Artist MapArtistRequestToArtist(ArtistRequest artistRequest)
        {
            Artist artist = new Artist
            {
                Name = artistRequest.Name,
                Info = artistRequest.Info,
                PicturePath = artistRequest.PicturePath,
                UserId = artistRequest.UserId,
                Songs = artistRequest.SongIds.Select(s => new ArtistSong
                {
                    MusicId = s
                }).ToList(),
                Links = artistRequest.LinksIds.Select(l => new ArtistLink
                {
                    LinkId = l
                }).ToList(),
            };

            return artist;
        }


        public async Task<List<ArtistResponse>> GetAllAsync()
        {
            List<Artist> artists = await _artistRepository.GetAllAsync();

            if (artists == null)
            {
                throw new ArgumentException();
            }
            return artists.Select(MapArtistToArtistResponse).ToList();
        }

        public async Task<ArtistResponse> FindByIdAsync(int artistId)
        {
            var artist = await _artistRepository.FindByIdAsync(artistId);

            if (artist != null)
            {
                return MapArtistToArtistResponse(artist);
            }

            return null;
        }

        public async Task<ArtistResponse> CreateAsync(ArtistRequest newArtist)
        {
            var artist = await _artistRepository.CreateAsync(MapArtistRequestToArtist(newArtist));
            if (artist == null)
            {
                throw new ArgumentNullException();
            }
            return MapArtistToArtistResponse(artist);
        }

        public async Task<ArtistResponse> DeleteByIdAsync(int artistId)
        {
            var artist = await _artistRepository.DeleteByIdAsync(artistId);

            if (artist != null)
            {
                return MapArtistToArtistResponse(artist);
            }
            return null;
        }

        public async Task<ArtistResponse> UpdateByIdAsync(int artistId, ArtistRequest updateArtist)
        {
            var artist = MapArtistRequestToArtist(updateArtist);
            var insertedArtist = await _artistRepository.UpdateByIdAsync(artistId, artist);

            if (insertedArtist != null)
            {
                return MapArtistToArtistResponse(insertedArtist);
            }

            return null;
        }

        public async Task<ArtistResponse> UploadPicture(int artistId, IFormFile file)
        {
            Artist artist = await _artistRepository.UploadPicture(artistId, file);

            if (artist != null)
            {
                return MapArtistToArtistResponse(artist);
            }

            return null;

        }
    }
}
