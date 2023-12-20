using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseHelpers
{
    public class DatabaseSeedHelper
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            SeedBirds(modelBuilder);
            SeedCats(modelBuilder);
            SeedDogs(modelBuilder);
            SeedUsers(modelBuilder);
        }

        private static void SeedBirds(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bird>().HasData(
                new Bird { Id = Guid.NewGuid(), Name = "Mitrovic", CanFly = true, Color = "White" },
                new Bird { Id = Guid.NewGuid(), Name = "Klose", CanFly = false, Color = "Red" },
                new Bird { Id = Guid.NewGuid(), Name = "Gomez", CanFly = true, Color = "Blue" }
                );
        }

        private static void SeedCats(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>().HasData(
                new Cat { Id = Guid.NewGuid(), Name = "Cambiasso", LikesToPlay = false, Breed = "Maine Coon", Weight = 12 },
                new Cat { Id = Guid.NewGuid(), Name = "Sneijder", LikesToPlay = true, Breed = "Bengal", Weight = 5 },
                new Cat { Id = Guid.NewGuid(), Name = "Santi Cazorla", LikesToPlay = true, Breed = "Burma", Weight = 7 }
                );
        }

        private static void SeedDogs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>().HasData(
                new Dog { Id = Guid.NewGuid(), Name = "Mertesacker", Breed = "Great Dane", Weight = 75 },
                new Dog { Id = Guid.NewGuid(), Name = "Nesta", Breed = "Berner senner", Weight = 35 },
                new Dog { Id = Guid.NewGuid(), Name = "Saliba", Breed = "Leonberger", Weight = 50 }
                );
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), UserName = "Admin", Password = "Boss" },
                new User { Id = Guid.NewGuid(), UserName = "noob", Password = "noob" }
                );
        }
    }
}
