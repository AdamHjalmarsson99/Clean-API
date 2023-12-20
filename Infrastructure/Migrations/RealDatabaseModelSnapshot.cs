﻿// <auto-generated />
using System;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(RealDatabase))]
    partial class RealDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Models.Bird", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("CanFly")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Birds");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d30022d8-53c4-4dfd-8610-6a67e6b8cd8b"),
                            CanFly = true,
                            Color = "White",
                            Name = "Mitrovic"
                        },
                        new
                        {
                            Id = new Guid("afae045c-960e-4b89-ab0b-3efef596c372"),
                            CanFly = false,
                            Color = "Red",
                            Name = "Klose"
                        },
                        new
                        {
                            Id = new Guid("cf727059-dc71-48c1-abd9-21f196658126"),
                            CanFly = true,
                            Color = "Blue",
                            Name = "Gomez"
                        });
                });

            modelBuilder.Entity("Domain.Models.Cat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LikesToPlay")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cats");

                    b.HasData(
                        new
                        {
                            Id = new Guid("62e1ae66-673f-4781-a72a-f141f0e99686"),
                            Breed = "Maine Coon",
                            LikesToPlay = false,
                            Name = "Cambiasso",
                            Weight = 12
                        },
                        new
                        {
                            Id = new Guid("ad32fd5a-d82a-4c06-8ac9-77978987b328"),
                            Breed = "Bengal",
                            LikesToPlay = true,
                            Name = "Sneijder",
                            Weight = 5
                        },
                        new
                        {
                            Id = new Guid("5b219ade-bee7-4eb8-9c08-7f23b04d7717"),
                            Breed = "Burma",
                            LikesToPlay = true,
                            Name = "Santi Cazorla",
                            Weight = 7
                        });
                });

            modelBuilder.Entity("Domain.Models.Dog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Dogs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1e2c5c6d-7f22-4aa0-a3f2-270a7d78acd2"),
                            Breed = "Great Dane",
                            Name = "Mertesacker",
                            Weight = 75
                        },
                        new
                        {
                            Id = new Guid("efe94c5e-0a9f-424d-afa5-1ff0bb49d1ba"),
                            Breed = "Berner senner",
                            Name = "Nesta",
                            Weight = 35
                        },
                        new
                        {
                            Id = new Guid("e5147d7f-efe9-4b5f-8b0d-831b9a565929"),
                            Breed = "Leonberger",
                            Name = "Saliba",
                            Weight = 50
                        });
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0a78f0a7-10f1-44f3-825f-0bf1349111c0"),
                            Password = "Boss",
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("073e8895-d72a-4531-acd0-0ff414b2ba93"),
                            Password = "noob",
                            UserName = "noob"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
