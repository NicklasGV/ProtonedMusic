namespace ProtonedMusicAPI.Authentication
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository accountRepository, IJwtUtils jwtUtils)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId is not null)
            {
                //Attach account to context on succesful jwt validation
                var account = await accountRepository.FindById(accountId.Value);
                context.Items["User"] = UserService.MapUserToUserResponse(account);
            }

            await _next(context);
        }
    }
}
