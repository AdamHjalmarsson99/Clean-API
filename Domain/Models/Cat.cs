using Domain.Models.Animal;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Cat : AnimalModel
    {
        [Required]
        public bool LikesToPlay { get; set; }
    }
}
