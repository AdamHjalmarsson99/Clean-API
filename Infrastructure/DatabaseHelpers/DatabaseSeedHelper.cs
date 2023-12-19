﻿using Domain.Models;
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
                new Bird { Id = Guid.NewGuid(), Name = "Mitrovic", CanFly = true },
                new Bird { Id = Guid.NewGuid(), Name = "Klose", CanFly = false },
                new Bird { Id = Guid.NewGuid(), Name = "Gomez", CanFly = true }
                );
        }

        private static void SeedCats(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>().HasData(
                new Cat { Id = Guid.NewGuid(), Name = "Cambiasso", LikesToPlay = false },
                new Cat { Id = Guid.NewGuid(), Name = "Sneijder", LikesToPlay = true },
                new Cat { Id = Guid.NewGuid(), Name = "Santi Cazorla", LikesToPlay = true }
                );
        }

        private static void SeedDogs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>().HasData(
                new Dog { Id = Guid.NewGuid(), Name = "Mertesacker" },
                new Dog { Id = Guid.NewGuid(), Name = "Nesta" },
                new Dog { Id = Guid.NewGuid(), Name = "Saliba" }
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
