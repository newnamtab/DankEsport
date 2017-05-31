using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EsportProject.Models.DBmodels;

namespace EsportProject.Migrations.Contact
{
    [DbContext(typeof(ContactContext))]
    [Migration("20170531095618_initialcreate")]
    partial class initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("EsportProject.Models.DBmodels.Contact", b =>
                {
                    b.Property<int>("ContactID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<string>("Contactmailadress");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Message");

                    b.HasKey("ContactID");

                    b.ToTable("Contact");
                });
        }
    }
}
