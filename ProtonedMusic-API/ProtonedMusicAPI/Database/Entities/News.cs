namespace ProtonedMusicAPI.Database.Entities
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(600)")]
        public string Text { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
