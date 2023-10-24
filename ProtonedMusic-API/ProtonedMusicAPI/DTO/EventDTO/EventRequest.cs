namespace ProtonedMusicAPI.DTO.EventDTO
{
    public class EventRequest
    {
        [Required]
        [StringLength(80, ErrorMessage = "Event Name cannot be longer than 80 characters")]
        public string Title { get; set; }
        [Required]
        [StringLength(600, ErrorMessage = "Description cannot be longer than 600 characters")]
        public string Description { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Price cannot be higher than 10000")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        [DataType (DataType.Date)]
        public DateTime TimeofEvent {  get; set; }
    }
}
