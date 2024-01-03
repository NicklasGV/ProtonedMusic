﻿namespace ProtonedMusicAPI.DTO.UserDTO
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public AddonRoles AddonRoles { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int? Postal { get; set; }
        public string? ProfilePicturePath { get; set; }

        public List<UserNewsLikeResponse> NewsLikes { get; set; } = new();
    }

    public class UserNewsLikeResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
