using Domain.Models.Animal;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Cat : AnimalModel
    {
        [Required]
        public bool LikesToPlay { get; set; }
        public string Breed { get; set; } = string.Empty;
        public int Weight { get; set; }
    }
}
