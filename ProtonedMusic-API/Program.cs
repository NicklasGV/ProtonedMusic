
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProtonedMusic.Repository.Database;
using ProtonedMusic.Repository.Repositories;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace ProtonedMusic_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication("YourAuthenticationScheme")
            .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("YourAuthenticationScheme", options => { });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("YourPolicyName", policy =>
                {
                    policy.RequireAuthenticatedUser(); // Example policy requiring authentication
                                                       // Add additional policy requirements as needed
                });
            });

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();



            builder.Services.AddDbContext<DatabaseContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("ConString"));
            });

            //Hvis man har mange til mange relationer, kan den godt finde på at blive ved med at lave lister med data.
            //man ender så med at få forkert data. Det stopper denne
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("policy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("policy");

            app.MapControllers();

            app.Run();
        }
    }

    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public CustomAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, "123456"), // Unique identifier for the user
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}