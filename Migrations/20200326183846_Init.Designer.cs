﻿// <auto-generated />
using FirstWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FirstWebApplication.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200326183846_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FirstWebApplication.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<string>("TeacherName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TeacherName");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("FirstWebApplication.Models.Teacher", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Discipline")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("FirstWebApplication.Models.Student", b =>
                {
                    b.HasOne("FirstWebApplication.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherName");
                });
#pragma warning restore 612, 618
        }
    }
}
