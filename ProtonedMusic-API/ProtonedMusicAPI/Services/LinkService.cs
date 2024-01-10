using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.DTO.EmailDTO;
using ProtonedMusicAPI.DTO.LinkDTO;
using ProtonedMusicAPI.Interfaces.ILink;

namespace ProtonedMusicAPI.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;


        public LinkService(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public static LinkResponse MapLinkToLinkResponse(Link link)
        {
            LinkResponse response = new LinkResponse
            {
                Id = link.Id,
                Title = link.Title,
                LinkAddress = link.LinkAddress,
                
            };
            if (link.Artist != null)
            {
                response.Artist = new LinkArtistResponse
                {
                    Id = link.Artist.Id,
                    Name = link.Artist.Name,
                    Info = link.Artist.Info,
                    PicturePath = link.Artist.PicturePath,

                };
            }
            return response;
        }

        private static Link MapLinkRequestToLink(LinkRequest linkRequest)
        {
            Link link = new Link
            {
                Title = linkRequest.Title,
                LinkAddress = linkRequest.LinkAddress,
                ArtistId = linkRequest.ArtistId
            };

            return link;
        }


        public async Task<List<LinkResponse>> GetAllAsync()
        {
            List<Link> links = await _linkRepository.GetAllAsync();

            if (links == null)
            {
                throw new ArgumentException();
            }
            return links.Select(MapLinkToLinkResponse).ToList();
        }

        public async Task<LinkResponse> FindByIdAsync(int linkId)
        {
            var link = await _linkRepository.FindByIdAsync(linkId);

            if (link != null)
            {
                return MapLinkToLinkResponse(link);
            }

            return null;
        }

        public async Task<LinkResponse> CreateAsync(LinkRequest newLink)
        {
            var link = await _linkRepository.CreateAsync(MapLinkRequestToLink(newLink));
            if (link == null)
            {
                throw new ArgumentNullException();
            }
            return MapLinkToLinkResponse(link);
        }

        public async Task<LinkResponse> DeleteByIdAsync(int linkId)
        {
            var link = await _linkRepository.DeleteByIdAsync(linkId);

            if (link != null)
            {
                return MapLinkToLinkResponse(link);
            }
            return null;
        }

        public async Task<LinkResponse> UpdateByIdAsync(int linkId, LinkRequest updateLink)
        {
            var link = MapLinkRequestToLink(updateLink);
            var insertedLink = await _linkRepository.UpdateByIdAsync(linkId, link);

            if (insertedLink != null)
            {
                return MapLinkToLinkResponse(insertedLink);
            }

            return null;
        }
    }
}
