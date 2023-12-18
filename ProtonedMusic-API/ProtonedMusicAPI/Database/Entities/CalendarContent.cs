namespace ProtonedMusicAPI.Database.Entities
{
    public class CalendarContent
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string FamilyMember { get; set; }
    }
}
