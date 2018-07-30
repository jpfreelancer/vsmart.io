﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SmartAdmin.Seed.Data;
using System;

namespace SmartAdmin.Seed.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180709164222_AddESRequestField")]
    partial class AddESRequestField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.Articles", b =>
                {
                    b.Property<int>("articlesID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SourcesID");

                    b.Property<string>("_Tags");

                    b.Property<string>("articleContent");

                    b.Property<string>("articleDescription");

                    b.Property<string>("articleTitle")
                        .IsRequired();

                    b.Property<string>("articleUrl");

                    b.Property<string>("imageUrl");

                    b.Property<DateTime>("lastModified");

                    b.Property<DateTime>("publishedDate");

                    b.HasKey("articlesID");

                    b.HasIndex("SourcesID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.ArticleVersions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleContent");

                    b.Property<string>("ArticleDescription");

                    b.Property<string>("ArticleTitle");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int?>("TasksID");

                    b.Property<string>("Url");

                    b.Property<int>("VersionNumber");

                    b.HasKey("ID");

                    b.HasIndex("TasksID");

                    b.ToTable("ArticleVersions");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.Sources", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SourceName")
                        .IsRequired();

                    b.Property<int>("SourceTypeId");

                    b.Property<string>("SourceUrl")
                        .IsRequired();

                    b.Property<string>("_SourceConfiguration");

                    b.HasKey("ID");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.TaskHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<int?>("TasksID");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("TasksID");

                    b.ToTable("TaskHistory");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.Tasks", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleContent");

                    b.Property<string>("ArticleDescription");

                    b.Property<string>("ArticleTitle");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Frequency");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Priority");

                    b.Property<string>("Status");

                    b.Property<string>("Url");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.Topics", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("ESRequest");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Rules");

                    b.Property<string>("SelectedSources");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status");

                    b.Property<string>("TopicName");

                    b.Property<string>("UserCreated");

                    b.Property<bool>("isFeatured");

                    b.HasKey("ID");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SmartAdmin.Seed.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SmartAdmin.Seed.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartAdmin.Seed.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SmartAdmin.Seed.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.Articles", b =>
                {
                    b.HasOne("SmartAdmin.Seed.Data.Entity.Sources")
                        .WithMany("Articles")
                        .HasForeignKey("SourcesID");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.ArticleVersions", b =>
                {
                    b.HasOne("SmartAdmin.Seed.Data.Entity.Tasks")
                        .WithMany("ArticleVersions")
                        .HasForeignKey("TasksID");
                });

            modelBuilder.Entity("SmartAdmin.Seed.Data.Entity.TaskHistory", b =>
                {
                    b.HasOne("SmartAdmin.Seed.Data.Entity.Tasks")
                        .WithMany("Histories")
                        .HasForeignKey("TasksID");
                });
#pragma warning restore 612, 618
        }
    }
}
