﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using crypto_api.Models;

#nullable disable

namespace crypto_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("crypto_api.Models.Client", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("crypto_api.Models.Transaction", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("client_dataid")
                        .HasColumnType("int");

                    b.Property<int>("client_id")
                        .HasColumnType("int");

                    b.Property<float>("crypto_amount")
                        .HasColumnType("real");

                    b.Property<string>("crypto_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("datetime")
                        .HasColumnType("datetime2");

                    b.Property<float>("money")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("client_dataid");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("crypto_api.Models.Transaction", b =>
                {
                    b.HasOne("crypto_api.Models.Client", "client_data")
                        .WithMany()
                        .HasForeignKey("client_dataid");

                    b.Navigation("client_data");
                });
#pragma warning restore 612, 618
        }
    }
}
