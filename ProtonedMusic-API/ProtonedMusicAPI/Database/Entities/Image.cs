namespace ProtonedMusicAPI.Database.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
