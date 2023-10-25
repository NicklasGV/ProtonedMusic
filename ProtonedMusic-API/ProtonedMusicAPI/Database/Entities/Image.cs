namespace ProtonedMusicAPI.Database.Entities
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ImageId { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;
    }
}
