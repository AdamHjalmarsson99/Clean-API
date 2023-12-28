using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Animal
{
    public class AnimalModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public List<User> Users { get; set; }
    }
}
