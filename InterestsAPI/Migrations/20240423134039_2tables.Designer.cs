﻿// <auto-generated />
using System;
using InterestsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InterestsAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240423134039_2tables")]
    partial class _2tables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InterestsAPI.Models.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterestId"));

                    b.Property<string>("InterestDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterestName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("InterestId");

                    b.HasIndex("PersonId");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("InterestsAPI.Models.Link", b =>
                {
                    b.Property<int>("LinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LinkId"));

                    b.Property<int?>("InterestId")
                        .HasColumnType("int");

                    b.Property<string>("Webbsite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LinkId");

                    b.HasIndex("InterestId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("InterestsAPI.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("PesronName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("InterestsAPI.Models.Interest", b =>
                {
                    b.HasOne("InterestsAPI.Models.Person", null)
                        .WithMany("Interests")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("InterestsAPI.Models.Link", b =>
                {
                    b.HasOne("InterestsAPI.Models.Interest", null)
                        .WithMany("Links")
                        .HasForeignKey("InterestId");
                });

            modelBuilder.Entity("InterestsAPI.Models.Interest", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("InterestsAPI.Models.Person", b =>
                {
                    b.Navigation("Interests");
                });
#pragma warning restore 612, 618
        }
    }
}
