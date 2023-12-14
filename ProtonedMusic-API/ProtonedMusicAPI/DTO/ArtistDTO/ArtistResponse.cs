namespace ProtonedMusicAPI.DTO.ArtistDTO
{
    public class ArtistResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string? PicturePath { get; set; }

        public ArtistUserResponse User { get; set; }

        public List<ArtistSongResponse> Songs { get; set; } = new();
        public List<ArtistLinkResponse> Links { get; set; } = new();
    }

    public class ArtistUserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Postal { get; set; }
    }

    public class ArtistSongResponse
    {
        public int Id { get; set; }
        public string SongName { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;

        public string SongFilePath { get; set; } = string.Empty;
        public string SongPicturePath { get; set; } = string.Empty;
    }
    public class ArtistLinkResponse
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string LinkAddress { get; set; } = string.Empty;
    }
}
