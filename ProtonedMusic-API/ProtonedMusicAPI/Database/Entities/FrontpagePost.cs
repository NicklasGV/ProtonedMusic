namespace ProtonedMusicAPI.Database.Entities
{
    public class FrontpagePost
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Text { get; set; }

        public Banner Banner { get; set; }

        public string? FrontpagePicturePath { get; set; }

    }
}
