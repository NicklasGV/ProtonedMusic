﻿namespace ProtonedMusicAPI.Database.Entities
{
    public class ItemProduct
    {
        [Key] public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
