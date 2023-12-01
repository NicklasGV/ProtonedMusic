using Microsoft.VisualBasic;

namespace ProtonedMusicAPI.Database.Entities
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(80)")]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } = 0;
        [Column(TypeName = "nvarchar(600)")]
        public string Description { get; set; } = string.Empty;
        public string? EventPicturePath { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime TimeofEvent { get; set; }
    }
}
