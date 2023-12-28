using Domain.Models.Animal;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<AnimalModel> Animals { get; set; }
    }
}
