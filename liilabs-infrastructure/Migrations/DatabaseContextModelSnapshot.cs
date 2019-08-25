﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using liivlabs_infrastructure.EntityFramework;

namespace liivlabs_infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
