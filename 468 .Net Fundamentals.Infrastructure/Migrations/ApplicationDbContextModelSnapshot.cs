﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _468_.Net_Fundamentals.Infrastructure;

namespace _468_.Net_Fundamentals.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Duedate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Index")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.CardAssign", b =>
                {
                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("AssignTo")
                        .HasColumnType("int");

                    b.HasKey("CardId", "AssignTo");

                    b.HasIndex("AssignTo");

                    b.ToTable("CardAssign");
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.CardTag", b =>
                {
                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("CardId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("CardTag");
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Project");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2022, 1, 8, 11, 15, 30, 273, DateTimeKind.Local).AddTicks(7541),
                            Name = "Project 1"
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = 2,
                            CreatedOn = new DateTime(2022, 1, 8, 11, 15, 30, 276, DateTimeKind.Local).AddTicks(3142),
                            Name = "Project 2"
                        });
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.ProjectMember", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AddOn")
                        .HasColumnType("datetime2");

                    b.HasKey("MemberId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectMember");
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Todo");
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "tronglt2001@gmail.com",
                            ImagePath = "https://lh6.googleusercontent.com/X7JYEBXkxFMLWlXgsipqGbOYN6j9Lh_83FdKL-WPAtVKZsNnwrEE-VJVR83IXO73jgq4NrVuwPER2JVgkuyIpFMDMLzN3kbY1uHnD2_5enIx52yB-0IWf_VIfgFcpQBb4Yp3-an0",
                            Name = "Tan Trong",
                            Role = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "hiennhu@gmail.com",
                            ImagePath = "https://i.pinimg.com/474x/15/06/df/1506df6aa1b4c6a8162683d7e8114e65.jpg",
                            Name = "Hien Nhu",
                            Role = 1
                        });
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Business", b =>
                {
                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.Project", "Project")
                        .WithMany("Businesses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Card", b =>
                {
                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.Business", "Business")
                        .WithMany("Cards")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.CardAssign", b =>
                {
                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("AssignTo")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.CardTag", b =>
                {
                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Project", b =>
                {
                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.ProjectMember", b =>
                {
                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Tag", b =>
                {
                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_468_.Net_Fundamentals.Domain.Entities.Todo", b =>
                {
                    b.HasOne("_468_.Net_Fundamentals.Domain.Entities.Card", "Card")
                        .WithMany("Todos")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
