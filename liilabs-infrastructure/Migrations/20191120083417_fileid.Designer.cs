﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using liivlabs_infrastructure.EntityFramework;

namespace liivlabs_infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191120083417_fileid")]
    partial class fileid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("liivlabs_shared.Entities.Account.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddedUsers");

                    b.Property<string>("EmailAddress");

                    b.Property<bool>("EmailVerified");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("MaxUsers");

                    b.Property<int>("ParentUserId");

                    b.Property<bool>("PasswordChanged");

                    b.Property<bool>("PasswordReset");

                    b.Property<string>("Role");

                    b.Property<byte[]>("passwordHash");

                    b.Property<byte[]>("passwordSalt");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("liivlabs_shared.Entities.File.FileAlertEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<int>("FileId");

                    b.Property<bool>("IsNew");

                    b.HasKey("Id");

                    b.ToTable("FileAlert");
                });

            modelBuilder.Entity("liivlabs_shared.Entities.File.FileEntity", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AudioFileName");

                    b.Property<string>("OriginalName");

                    b.Property<decimal>("OriginalSize");

                    b.Property<string>("Text");

                    b.Property<string>("UserEmail");

                    b.Property<string>("VideoFileName");

                    b.Property<DateTime>("createdAt");

                    b.Property<DateTime>("editedAt");

                    b.HasKey("FileId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("liivlabs_shared.Entities.UserAccessEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Create");

                    b.Property<bool>("Delete");

                    b.Property<bool>("Edit");

                    b.Property<int>("UserId");

                    b.Property<bool>("View");

                    b.HasKey("id");

                    b.ToTable("UserAccess");
                });
#pragma warning restore 612, 618
        }
    }
}
