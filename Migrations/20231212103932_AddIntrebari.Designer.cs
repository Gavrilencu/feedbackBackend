// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using feedback.Data;

#nullable disable

namespace feedback.Migrations
{
    [DbContext(typeof(FeedbackContext))]
    [Migration("20231212103932_AddIntrebari")]
    partial class AddIntrebari
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("feedback.Models.Chestionar", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nume")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tip")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Chestionare");
                });

            modelBuilder.Entity("feedback.Models.Intrebare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DenumireIntrebare")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdChestionar")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Intrebari");
                });
#pragma warning restore 612, 618
        }
    }
}
