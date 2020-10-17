﻿// <auto-generated />
using System;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20201017193252_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Blog.Models.Post", b =>
                {
                    b.Property<string>("guid")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("body");

                    b.Property<DateTime>("createdAt");

                    b.Property<string>("description");

                    b.Property<string>("slug");

                    b.Property<string>("title");

                    b.Property<DateTime>("updatedAt");

                    b.HasKey("guid");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Blog.Models.Tag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Postguid");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.HasIndex("Postguid");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Blog.Models.Tag", b =>
                {
                    b.HasOne("Blog.Models.Post")
                        .WithMany("tagList")
                        .HasForeignKey("Postguid");
                });
#pragma warning restore 612, 618
        }
    }
}