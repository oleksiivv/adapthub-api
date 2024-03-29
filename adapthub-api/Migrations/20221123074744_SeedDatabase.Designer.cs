﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using adapthub_api;
using adapthub_api.Models;
using adapthub_api.ViewModels;

#nullable disable

namespace adapthub_api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221123074744_SeedDatabase")]
    partial class SeedDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("adapthub_api.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("IDCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("adapthub_api.Models.CustomerExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Experience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PastJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profession")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerExperience");
                });

            modelBuilder.Entity("adapthub_api.Models.JobRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ExpectedSalary")
                        .HasColumnType("int");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("JobRequests");
                });

            modelBuilder.Entity("adapthub_api.Models.Moderator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Moderators");
                });

            modelBuilder.Entity("adapthub_api.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EDRPOU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("adapthub_api.Models.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ChosenJobRequestId")
                        .HasColumnType("int");

                    b.Property<int>("MinExperience")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChosenJobRequestId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("adapthub_api.Models.Customer", b =>
                {
                    b.HasOne("adapthub_api.Models.CustomerExperience", "Experience")
                        .WithMany()
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experience");
                });

            modelBuilder.Entity("adapthub_api.Models.JobRequest", b =>
                {
                    b.HasOne("adapthub_api.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("adapthub_api.Models.Vacancy", b =>
                {
                    b.HasOne("adapthub_api.Models.JobRequest", "ChosenJobRequest")
                        .WithMany()
                        .HasForeignKey("ChosenJobRequestId");

                    b.HasOne("adapthub_api.Models.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChosenJobRequest");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("adapthub_api.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("adapthub_api.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("adapthub_api.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("adapthub_api.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });


            
#pragma warning restore 612, 618
        }
    }
}

/*
 * modelBuilder.Entity<Organization>()
                .HasData(
                    new Organization
                    {
                        Id = 1,
                        Name = "Google",
                        SiteLink = "google.com",
                        Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                        EDRPOU = "43700200",
                        Email = "temail@google.com",
                        PasswordHash = "12975910c3e6352b5b2bdee81fa2fc4653a5bd59",
                    },
                    new Organization
                    {
                        Id = 2,
                        Name = "Facebook",
                        SiteLink = "facebook.com",
                        Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                        EDRPOU = "43700500",
                        Email = "temail@facebook.com",
                        PasswordHash = "bbg75910c3e6352b5b2bdee81fa2fc4653a5bd87",
                    },
                    new Organization
                    {
                        Id = 3,
                        Name = "Disney",
                        SiteLink = "disney.com",
                        Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                        EDRPOU = "43900200",
                        Email = "temail@disney.com",
                        PasswordHash = "6g575910c3e6352b5b2bdee81fa2fc4653a5bd88",
                    },
                    new Organization
                    {
                        Id = 4,
                        Name = "Warner Bros Discovery",
                        SiteLink = "wbd.com",
                        Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                        EDRPOU = "4370990",
                        Email = "wbdd654@wbd.com",
                        PasswordHash = "hjg75910c3e6352b5b2bdee81fa2fc4653a5ghhy",
                    }
                );

            modelBuilder.Entity<Customer>()
                .HasData(
                    new Customer
                    {
                        Id = 1,
                        Gender = GenderType.male,
                        UserName = "Петро Петренко",
                        Email = "ppetrenko@gmail.com",
                        EmailConfirmed = true,
                        PasswordHash = "32af2719dfacf9f199795043b3032177518d544c",
                        PassportNumber = "0098876541",
                        IDCode = "12457293",
                        CurrentAddress = "Львів, вул. Скрипника 18",
                        PhoneNumber = "+380985463746",
                        Experience = new CustomerExperience
                        {
                            Id = 1,
                            PastJob = "Freelance",
                            Education = "НУ ЛП, спеціальність - ІПЗ, бакалавр, рік закінчення - 2021",
                            Experience = "Freelance - рік",
                            Profession = "Інженер ПЗ, напрямок - Data Science"
                        }
                    },
                    new Customer
                    {
                        Id = 2,
                        Gender = GenderType.male,
                        UserName = "Микола Андрієнко",
                        Email = "mm12d@gmail.com",
                        EmailConfirmed = true,
                        PasswordHash = "249ae5af1f3a0742fff7e16728d8b962d28d1d01",
                        PassportNumber = "0085076541",
                        IDCode = "78657293",
                        CurrentAddress = "Львів, вул. Коперника 18",
                        PhoneNumber = "+380967834526",
                        Experience = new CustomerExperience
                        {
                            Id = 2,
                            PastJob = "Касир у MCDonalds",
                            Education = "Повна середня",
                            Experience = "2 роки",
                            Profession = "Різноробочий"
                        }
                    },
                    new Customer
                    {
                        Id = 3,
                        Gender = GenderType.female,
                        UserName = "Софія Тополя",
                        Email = "ss1s67@gmail.com",
                        EmailConfirmed = true,
                        PasswordHash = "14d05512336757d9e18c12b3bb4d1baba4a184b8",
                        PassportNumber = "0085076541",
                        IDCode = "78657293",
                        CurrentAddress = "Львів, вул. Коперника 18",
                        PhoneNumber = "+380967834526",
                        Experience = new CustomerExperience
                        {
                            Id = 3,
                            PastJob = "Epam Systems",
                            Education = "НУ ЛП, спеціальність - КН, бакалавр, рік закінчення - 2019",
                            Experience = "Epam Systems - 1.5 року, Freelance - 20 місяців",
                            Profession = "Спец. у галузі КН, напрямок - BE"
                        }
                    }
            );

            modelBuilder.Entity<Moderator>()
                .HasData(
                    new Moderator
                    {
                        Id = 1,
                        FullName = "Василь Сало",
                        PasswordHash = "14d05512336757d9e18c12b3bb4d1baba4a184b8",
                        Email = "admin@gmail.com",
                        PhoneNumber = "+380987847587"
                    }
            );

            modelBuilder.Entity<Vacancy>()
                .HasData(
                    new Vacancy
                    {
                        Id = 1,
                        Organization = new Organization
                        {
                            Id = 5,
                            Name = "АТБ",
                            SiteLink = "atb.com",
                            Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                            EDRPOU = "43700200",
                            Email = "atb_worker@gmail.com",
                            PasswordHash = "12975910c3e6352b5b2bdee81fa2fc4653a5bd59",
                        },
                        Status = StatusType.Confirmed,
                        Speciality = "Працівник на склад",
                        Salary = 10000,
                        MinExperience = 12
                    },
                    new Vacancy
                    {
                        Id = 2,
                        Organization = new Organization
                        {
                            Id = 6,
                            Name = "Рукавичка",
                            SiteLink = "sitetest.com",
                            Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                            EDRPOU = "43700200",
                            Email = "rykavuchka@gmail.com",
                            PasswordHash = "12975910c3e6352b5b2bdee81fa2fc4653a5bd59",
                        },
                        Status = StatusType.Confirmed,
                        Speciality = "Касир",
                        Salary = 10000,
                        MinExperience = 0
                    },
                    new Vacancy
                    {
                        Id = 3,
                        Organization = new Organization
                        {
                            Id = 7,
                            Name = "Спортлайф",
                            SiteLink = "sportlige.com",
                            Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                            EDRPOU = "43700200",
                            Email = "rykavuchka@gmail.com",
                            PasswordHash = "12975910c3e6352b5b2bdee81fa2fc4653a5bd59",
                        },
                        Status = StatusType.InReview,
                        Speciality = "Прибиральниця",
                        Salary = 7000,
                        MinExperience = 0
                    }
            );

            modelBuilder.Entity<JobRequest>()
                .HasData(
                    new JobRequest
                    {
                        Id = 1,
                        Customer = new Customer
                        {
                            Id = 5,
                            Gender = GenderType.male,
                            UserName = "Іван Миколайчук",
                            Email = "johnm@gmail.com",
                            EmailConfirmed = true,
                            PasswordHash = "32af2719dfacf9f199795043b3032177518d544c",
                            PassportNumber = "0098876541",
                            IDCode = "12457293",
                            CurrentAddress = "Львів, вул. Скрипника 18",
                            PhoneNumber = "+380985463746",
                            Experience = new CustomerExperience
                            {
                                Id = 5,
                                PastJob = "Працівник на складі",
                                Education = "Повна вища",
                                Experience = "Працівник на складі - рік",
                                Profession = "Різноробочий"
                            }
                        },
                        Status = StatusType.Confirmed,
                        Speciality = "Працівник на складі",
                        ExpectedSalary = 10000
                    },
                    new JobRequest
                    {
                        Id = 2,
                        Customer = new Customer
                        {
                            Id = 6,
                            Gender = GenderType.male,
                            UserName = "Ірина Середа",
                            Email = "seredai@gmail.com",
                            EmailConfirmed = true,
                            PasswordHash = "32af2719dfacf9f199795043b3032177518d544c",
                            PassportNumber = "0098876541",
                            IDCode = "12457293",
                            CurrentAddress = "Львів, вул. Сихівська 56",
                            PhoneNumber = "+380985555746",
                            Experience = new CustomerExperience
                            {
                                Id = 6,
                                PastJob = "Касир",
                                Education = "Повна вища",
                                Experience = "Касир - рік",
                                Profession = "Бугалтер"
                            }
                        },
                        Status = StatusType.Confirmed,
                        Speciality = "Касир",
                        ExpectedSalary = 7000
                    },
                    new JobRequest
                    {
                        Id = 3,
                        Customer = new Customer
                        {
                            Id = 7,
                            Gender = GenderType.male,
                            UserName = "Данило Дуб",
                            Email = "dubbd@gmail.com",
                            EmailConfirmed = true,
                            PasswordHash = "32af2719dfacf9f199795043b3032177518d544c",
                            PassportNumber = "0098876841",
                            IDCode = "12457293",
                            CurrentAddress = "Львів, вул. Зелена 44",
                            PhoneNumber = "+380985555746",
                            Experience = new CustomerExperience
                            {
                                Id = 7,
                                PastJob = "Відсутня",
                                Education = "Повна вища",
                                Experience = "Відсутній",
                                Profession = "Кухар-кондитер"
                            }
                        },
                        Status = StatusType.Confirmed,
                        Speciality = "Працівник на кухню",
                        ExpectedSalary = 7000
                    }
            );
*/