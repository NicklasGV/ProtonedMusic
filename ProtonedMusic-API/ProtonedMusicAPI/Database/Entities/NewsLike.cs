namespace ProtonedMusicAPI.Database.Entities
{
    public class NewsLike
    {
        [Key]
        public int Id { get; set; }

        public int user_Id { get; set; }
        public User User { get; set; }

        public int news_Id { get; set; }
        public News News { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

    }
}
