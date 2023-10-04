using System.ComponentModel.DataAnnotations;

namespace ProtonedMusicAPI.DTO.Category
{
    public class CategoryRequest
    {
        [Required]
        [StringLength(80, ErrorMessage = "Category name cannot be longer")]
        public string Name { get; set; } = string.Empty;
    }
}
