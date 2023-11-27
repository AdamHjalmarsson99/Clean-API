using Domain.Models;

namespace Infrastructure.Database
{
    public class MockDatabase
    {
        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }

        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }

        private static List<Dog> allDogs = new()
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"},
            new Dog { Id = new Guid("4f54036c-2faf-4c3a-b855-1db48d42f2b4"), Name = "TestDogForUpdateTest"},
            new Dog { Id = new Guid("6ec82d4a-49e7-41b1-a438-845043ee7fc7"), Name = "TestDogForDeleteTest"}
        };

        private static List<Bird> allBirds = new()
        {
            new Bird { Id = Guid.NewGuid(), Name = "Bergkamp", CanFly = false },
            new Bird { Id = Guid.NewGuid(), Name = "Rooney", CanFly = true },
            new Bird { Id = Guid.NewGuid(), Name = "Cambiasso", CanFly = false },
        };
    }
}
