﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movies.Api.Data;

#nullable disable

namespace Movies.Api.Migrations
{
    [DbContext(typeof(MoviesContext))]
    partial class MoviesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Movie_Actor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("Movie_Actor");
                });

            modelBuilder.Entity("Movies.Api.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Movies.Api.Models.ExternalInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImdbUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("RottenTomatoesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TmdbUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId")
                        .IsUnique();

                    b.ToTable("ExternalInformation");
                });

            modelBuilder.Entity("Movies.Api.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Movies.Api.Models.Movie", b =>
                {
                    b.Property<int>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Identifier"));

                    b.Property<int>("AgeRating")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<decimal>("InternetRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MainGenreName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("char(8)");

                    b.Property<string>("Synopsis")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Plot");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar");

                    b.HasKey("Identifier");

                    b.HasAlternateKey("Title", "ReleaseDate");

                    b.HasIndex("MainGenreName");

                    b.ToTable("Pictures", (string)null);

                    b.HasDiscriminator().HasValue("Movie");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Movies.Api.Models.CinemaMovie", b =>
                {
                    b.HasBaseType("Movies.Api.Models.Movie");

                    b.Property<decimal>("GrossRevenue")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue("CinemaMovie");
                });

            modelBuilder.Entity("Movies.Api.Models.TelevisionMovie", b =>
                {
                    b.HasBaseType("Movies.Api.Models.Movie");

                    b.Property<string>("ChannelFirstAiredOn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("TelevisionMovie");
                });

            modelBuilder.Entity("Movie_Actor", b =>
                {
                    b.HasOne("Movies.Api.Models.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MovieActor_Actor");

                    b.HasOne("Movies.Api.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MovieActor_Movie");
                });

            modelBuilder.Entity("Movies.Api.Models.ExternalInformation", b =>
                {
                    b.HasOne("Movies.Api.Models.Movie", "Movie")
                        .WithOne("ExternalInformation")
                        .HasForeignKey("Movies.Api.Models.ExternalInformation", "MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Movies.Api.Models.Movie", b =>
                {
                    b.HasOne("Movies.Api.Models.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("MainGenreName")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Movies.Api.Models.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("Movies.Api.Models.Movie", b =>
                {
                    b.Navigation("ExternalInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
