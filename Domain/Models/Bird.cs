using Domain.Models.Animal;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Bird : AnimalModel
    {
        [Required]
        public bool CanFly { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}
