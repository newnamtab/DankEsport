using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EsportProject.Models.DBmodels;

namespace EsportProject.Migrations.Turnament
{
    [DbContext(typeof(TurnamentContext))]
    partial class TurnamentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("EsportProject.Models.DBmodels.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ShorntenedName");

                    b.Property<string>("Spillere");

                    b.HasKey("TeamID");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("EsportProject.Models.DBmodels.TeamStanding", b =>
                {
                    b.Property<int>("TeamStandingID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LostMatches");

                    b.Property<int?>("TeamID");

                    b.Property<int?>("TurnamentID");

                    b.Property<int>("WonMatches");

                    b.Property<int>("drawMatches");

                    b.HasKey("TeamStandingID");

                    b.HasIndex("TeamID");

                    b.HasIndex("TurnamentID");

                    b.ToTable("TeamStanding");
                });

            modelBuilder.Entity("EsportProject.Models.DBmodels.Turnament", b =>
                {
                    b.Property<int>("TurnamentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<DateTime>("Slutdate");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("TurnamentID");

                    b.ToTable("Turnament");
                });

            modelBuilder.Entity("EsportProject.Models.DBmodels.TeamStanding", b =>
                {
                    b.HasOne("EsportProject.Models.DBmodels.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamID");

                    b.HasOne("EsportProject.Models.DBmodels.Turnament")
                        .WithMany("Standings")
                        .HasForeignKey("TurnamentID");
                });
        }
    }
}
