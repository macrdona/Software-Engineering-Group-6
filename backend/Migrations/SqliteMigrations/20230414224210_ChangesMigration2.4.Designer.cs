﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Models;

#nullable disable

namespace backend.Migrations.SqliteMigrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230414224210_ChangesMigration2.4")]
    partial class ChangesMigration24
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.13");

            modelBuilder.Entity("backend.Entities.CompletedCourses", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CourseId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "CourseId");

                    b.ToTable("CompletedCourses");
                });

            modelBuilder.Entity("backend.Entities.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Availability")
                        .HasColumnType("TEXT");

                    b.Property<string>("CoRequisites")
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Credits")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Prerequisites")
                        .HasColumnType("TEXT");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("backend.Entities.EnrollmentForm", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Major")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Minor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("OralRequirementComplete")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("backend.Entities.Majors", b =>
                {
                    b.Property<int>("MajorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Degree")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("MajorName")
                        .HasColumnType("TEXT");

                    b.HasKey("MajorId");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("backend.Entities.Requirements", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CoreRequirements")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MajorElectives")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MajorRequirements")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OralRequirement")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Prerequisites")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("StateRequirements")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UNFRequirements")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Requirements");
                });

            modelBuilder.Entity("backend.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Completed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
