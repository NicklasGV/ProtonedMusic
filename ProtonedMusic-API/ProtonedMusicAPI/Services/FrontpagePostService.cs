namespace ProtonedMusicAPI.Services
{
    public class FrontpagePostService : IFrontpagePostService
    {
        private readonly IFrontpagePostRepository _frontpagePostRepository;
        private readonly IJwtUtils _jwtUtils;


        public FrontpagePostService(IFrontpagePostRepository frontpagePostRepository, IJwtUtils jwtUtils)
        {
            _frontpagePostRepository = frontpagePostRepository;
            _jwtUtils = jwtUtils;
        }

        public static FrontpagePostResponse MapFrontpageToFrontpageResponse(FrontpagePost frontpage)
        {
            FrontpagePostResponse response = new FrontpagePostResponse
            {
                Id = frontpage.Id,
                Text = frontpage.Text,
                Banner = frontpage.Banner,
                FrontpagePicturePath = frontpage.FrontpagePicturePath,
            };
            return response;
        }

        private static FrontpagePost MapFrontpageRequestToFrontpage(FrontpagePostRequest frontpageRequest)
        {
            FrontpagePost user = new FrontpagePost
            {
                Text = frontpageRequest.Text,
                Banner = frontpageRequest.Banner,
                FrontpagePicturePath = frontpageRequest.FrontpagePicturePath ?? string.Empty,
            };
            return user;
        }

        public async Task<List<FrontpagePostResponse>> GetAllAsync()
        {
            List<FrontpagePost> frontpages = await _frontpagePostRepository.GetAllAsync();

            if (frontpages == null)
            {
                throw new ArgumentException();
            }
            return frontpages.Select(MapFrontpageToFrontpageResponse).ToList();
        }

        public async Task<FrontpagePostResponse> FindByIdAsync(int frontpageId)
        {
            var frontpage = await _frontpagePostRepository.FindByIdAsync(frontpageId);

            if (frontpage != null)
            {
                return MapFrontpageToFrontpageResponse(frontpage);
            }

            return null;
        }

        public async Task<FrontpagePostResponse> CreateAsync(FrontpagePostRequest newFrontpage)
        {
            var frontpage = await _frontpagePostRepository.CreateAsync(MapFrontpageRequestToFrontpage(newFrontpage));
            if (frontpage == null)
            {
                throw new ArgumentNullException();
            }
            return MapFrontpageToFrontpageResponse(frontpage);
        }

        public async Task<FrontpagePostResponse> DeleteByIdAsync(int frontpageId)
        {
            var frontpage = await _frontpagePostRepository.DeleteByIdAsync(frontpageId);

            if (frontpage != null)
            {
                return MapFrontpageToFrontpageResponse(frontpage);
            }
            return null;
        }

        public async Task<FrontpagePostResponse> UpdateByIdAsync(int frontpageId, FrontpagePostRequest updateFrontpage)
        {
            var frontpage = MapFrontpageRequestToFrontpage(updateFrontpage);
            var insertedFrontpage = await _frontpagePostRepository.UpdateByIdAsync(frontpageId, frontpage);

            if (insertedFrontpage != null)
            {
                return MapFrontpageToFrontpageResponse(insertedFrontpage);
            }

            return null;
        }

        public async Task<FrontpagePostResponse> UploadFrontpagePicture(int frontpageId, IFormFile file)
        {
            FrontpagePost frontpage = await _frontpagePostRepository.UploadFrontpagePicture(frontpageId, file);

            if (frontpage != null)
            {
                return MapFrontpageToFrontpageResponse(frontpage);
            }

            return null;

        }
    }
}
