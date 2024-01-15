using System.ComponentModel.DataAnnotations;

namespace myprojectEF.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nom { get; set; } = "";
        
        [MaxLength(100)]
        public string Brand { get; set; } = "";

        [MaxLength(100)]

        public string Categorie { get; set; } = "";

        public double Prix { get; set; }

        public string Description { get; set; } = "";

        [MaxLength(100)] 
        public string ImageFileName { get; set; } = "";

        public DateTime CreatedAt { get; set; }

    }
}
