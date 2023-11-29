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

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        public List<User> Users
        {
            get { return allUsers; }
            set { allUsers = value; }
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
            new Bird { Id = new Guid("60fdbc14-3f6c-4ddb-90a5-89c2e465be12"), Name = "TestBirdForUnitTests", CanFly = true },
            new Bird { Id = new Guid("65113f59-a1c8-4c0d-9215-cf407efb2108"), Name = "TestBirdForUpdateTests", CanFly = false },
            new Bird { Id = new Guid("94d807a1-5de4-4779-9f97-dd6a7173d207"), Name = "TestBirdForDeleteTests", CanFly = false }
        };

        private static List<Cat> allCats = new()
        {
            new Cat { Id = Guid.NewGuid(), Name = "Rice", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Saliba", LikesToPlay = true },
            new Cat { Id = Guid.NewGuid(), Name = "Jesus", LikesToPlay = true },
            new Cat { Id = new Guid("bf49c5e3-e438-42e7-8f2f-4a6d3656757d"), Name = "TestCatForUnitTests", LikesToPlay = true },
            new Cat { Id = new Guid("e4490bed-d15e-4d80-84e7-239dd90bf587"), Name = "TestCatForUpdateTests", LikesToPlay = false },
            new Cat { Id = new Guid("559c67b0-7baf-45cf-980f-5d424c142b69"), Name = "TestCatForDeleteTests", LikesToPlay = true }
        };

        private static List<User> allUsers = new()
        {
            new User{UserName = "Admin", Password = "Boss"},
            new User{UserName = "noob", Password = "noob"}
        };
    }
}
