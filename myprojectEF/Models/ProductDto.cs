using System.ComponentModel.DataAnnotations;

namespace myprojectEF.Models
{
    public class ProductDto
    {
        [Required ,MaxLength(100)]
        public string Nom { get; set; } = "";

        [Required, MaxLength(100)]
        public string Brand { get; set; } = "";

        [Required,MaxLength(100)]

        public string Categorie { get; set; } = "";

        [Required]
        public double Prix { get; set; }

        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
