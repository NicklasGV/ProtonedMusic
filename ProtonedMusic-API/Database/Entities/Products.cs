namespace ProtonedMusic_API.Database.Entities
{
    public class Products
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "nvarhcar(32)")]
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string ProductCategory { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string ProductDescription { get; set; } = string.Empty;

        [Column(TypeName = "smallint")]
        public int ProductPrice { get; set; } = 0;

        [Column(TypeName = "smallint")]
        public short ProductDate { get; set; } = 0;

    }
}
