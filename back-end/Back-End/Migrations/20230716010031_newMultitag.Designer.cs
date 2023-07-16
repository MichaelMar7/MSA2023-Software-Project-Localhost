﻿// <auto-generated />
using Back_End.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Back_End.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20230716010031_newMultitag")]
    partial class newMultitag
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Back_End.Models.BsPlayerTag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserDataId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserDataId");

                    b.ToTable("BsPlayerTags");
                });

            modelBuilder.Entity("Back_End.Models.CocPlayerTag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserDataId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserDataId");

                    b.ToTable("CocPlayerTags");
                });

            modelBuilder.Entity("Back_End.Models.CrPlayerTag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserDataId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserDataId");

                    b.ToTable("CrPlayerTags");
                });

            modelBuilder.Entity("Back_End.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Back_End.Models.UserData", b =>
                {
                    b.Property<string>("UserDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserDataId");

                    b.ToTable("UserDatas");
                });

            modelBuilder.Entity("Back_End.Models.BsPlayerTag", b =>
                {
                    b.HasOne("Back_End.Models.UserData", null)
                        .WithMany("BsPlayerTags")
                        .HasForeignKey("UserDataId");
                });

            modelBuilder.Entity("Back_End.Models.CocPlayerTag", b =>
                {
                    b.HasOne("Back_End.Models.UserData", null)
                        .WithMany("CocPlayerTags")
                        .HasForeignKey("UserDataId");
                });

            modelBuilder.Entity("Back_End.Models.CrPlayerTag", b =>
                {
                    b.HasOne("Back_End.Models.UserData", null)
                        .WithMany("CrPlayerTags")
                        .HasForeignKey("UserDataId");
                });

            modelBuilder.Entity("Back_End.Models.UserData", b =>
                {
                    b.Navigation("BsPlayerTags");

                    b.Navigation("CocPlayerTags");

                    b.Navigation("CrPlayerTags");
                });
#pragma warning restore 612, 618
        }
    }
}
