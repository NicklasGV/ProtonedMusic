namespace ProtonedMusicAPI.Database.Entities
{
    public class Upcoming
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(600)")]
        public string Description { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime timeOf { get; set; }
    }
}
