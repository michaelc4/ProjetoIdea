﻿// <auto-generated />
using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210409153325_UsuarioMigration_ChangePasswordLenght")]
    partial class UsuarioMigration_ChangePasswordLenght
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Api.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Admin")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DesEmail")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DesEspecialidade")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("DesExperiencia")
                        .HasMaxLength(3000)
                        .HasColumnType("longtext");

                    b.Property<string>("DesImagem")
                        .HasColumnType("LONGTEXT");

                    b.Property<string>("DesSenha")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("DesTelefone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Local")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DesEmail")
                        .IsUnique();

                    b.ToTable("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
