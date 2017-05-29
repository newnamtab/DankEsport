using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EsportProject.Models.DBmodels;

namespace EsportProject.Migrations
{
    [DbContext(typeof(NewsContext))]
    [Migration("20170526090655_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("EsportProject.Models.DBmodels.News", b =>
                {
                    b.Property<int>("NewsID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Title");

                    b.Property<string>("imgURL");

                    b.Property<string>("subtitle");

                    b.HasKey("NewsID");

                    b.ToTable("News");
                });
        }
    }
}
