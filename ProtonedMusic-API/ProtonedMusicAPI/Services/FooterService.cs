using ProtonedMusicAPI.DTO.FooterPostDTO;
using ProtonedMusicAPI.Interfaces.IFooter;

namespace ProtonedMusicAPI.Services
{
    public class FooterService : IFooterService
    {
        private readonly IFooterRepository _footerRepository;
        public FooterService(IFooterRepository footerRepository)
        {

            _footerRepository = footerRepository;

        }

        private static FooterResponse MapFooterToFooterResponse(FooterPost post)
        {
            FooterResponse response = new FooterResponse
            {
                Id = post.Id,
                Description = post.Description,
                Address = post.Address,
                AddressMapLink = post.AddressMapLink,
                Mail = post.Mail,
                Phonenumber = post.Phonenumber,
            };
            return response;
        }

        private static FooterPost MapFooterRequestToFooter(FooterRequest request)
        {
            return new FooterPost
            {
                Description = request.Description,
                Address = request.Address,
                AddressMapLink = request.AddressMapLink,
                Mail = request.Mail,
                Phonenumber = request.Phonenumber,
            };
        }

        public async Task<FooterResponse> CreateAsync(FooterRequest newFooterPost)
        {
            var post = await _footerRepository.CreateAsync(MapFooterRequestToFooter(newFooterPost));
            if (post == null)
            {
                throw new ArgumentNullException();
            }
            return MapFooterToFooterResponse(post);
        }

        public async Task<FooterResponse> DeleteByIdAsync(int footerId)
        {
            var post = await _footerRepository.DeleteByIdAsync(footerId);
            if (post != null)
            {
                return MapFooterToFooterResponse(post);
            }
            return null;
        }

        public async Task<FooterResponse?> FindByIdAsync(int footerId)
        {
            var post = await _footerRepository.FindByIdAsync(footerId);

            if(post != null)
            {
                return MapFooterToFooterResponse(post);
            }
            return null;
        }

        public async Task<List<FooterResponse>> GetAllAsync()
        {
            List<FooterPost> posts = await _footerRepository.GetAllAsync();
            if (posts == null)
            {
                throw new ArgumentNullException();
            }
            return posts.Select(MapFooterToFooterResponse).ToList();
        }

        public async Task<FooterResponse?> UpdateByIdAsync(int footerId, FooterRequest updateFooterPost)
        {
            var post = await _footerRepository.UpdateByIdAsync(footerId, MapFooterRequestToFooter(updateFooterPost));
            
            if (post != null)
            {
                return MapFooterToFooterResponse(post);
            }
            return null;
        }
    }
}
