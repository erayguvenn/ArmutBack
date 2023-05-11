using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArmutReborn.Models
{
    public partial class ArmutContext : DbContext
    {
        public ArmutContext()
        {
        }

        public ArmutContext(DbContextOptions<ArmutContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bid> Bids { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WorkListing> WorkListings { get; set; } = null!;
        public virtual DbSet<Workcategory> Workcategories { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=3.127.53.229;uid=Eray;pwd=armut;database=Armut", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.12-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("bid");

                entity.HasIndex(e => e.WorkerId, "worker_id");

                entity.HasIndex(e => e.WorklistingId, "worklisting_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Accepted).HasColumnName("accepted");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Message)
                    .HasMaxLength(2000)
                    .HasColumnName("message");

                entity.Property(e => e.Price)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("price");

                entity.Property(e => e.WorkerId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("worker_id");

                entity.Property(e => e.WorklistingId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("worklisting_id");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("bid_ibfk_1");

                entity.HasOne(d => d.Worklisting)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.WorklistingId)
                    .HasConstraintName("bid_ibfk_2");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("job");

                entity.HasIndex(e => e.WorkListingId, "WORK_LISTING_INDEX");

                entity.HasIndex(e => e.EmployerId, "job_employer_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("create_at")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.EmployerId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("employer_id");

                entity.Property(e => e.State)
                    .HasColumnType("enum('waiting_create_approval','waiting_payment','ongoing','worker_approved','completed','cancelled')")
                    .HasColumnName("state");

                entity.Property(e => e.WorkListingId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("work_listing_id");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_employer_id_foreign");

                entity.HasOne(d => d.WorkListing)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.WorkListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_work_listing_id_foreign");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.HasIndex(e => e.AlanId, "alanId");

                entity.HasIndex(e => e.BidId, "bidId");

                entity.HasIndex(e => e.GonderenId, "gonderenId");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AlanId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("alanId");

                entity.Property(e => e.BidId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("bidId");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.GonderenId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("gonderenId");

                entity.Property(e => e.SentDate)
                    .HasColumnType("timestamp")
                    .HasColumnName("sentDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.HasOne(d => d.Alan)
                    .WithMany(p => p.MessageAlans)
                    .HasForeignKey(d => d.AlanId)
                    .HasConstraintName("message_ibfk_2");

                entity.HasOne(d => d.Bid)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.BidId)
                    .HasConstraintName("message_ibfk_3");

                entity.HasOne(d => d.Gonderen)
                    .WithMany(p => p.MessageGonderens)
                    .HasForeignKey(d => d.GonderenId)
                    .HasConstraintName("message_ibfk_1");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("rating");

                entity.HasIndex(e => e.JobId, "rating_job_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.JobId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("job_id");

                entity.Property(e => e.Star)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("star");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rating_job_id_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .HasColumnName("surname");

                entity.Property(e => e.UserType)
                    .HasColumnType("enum('admin','user','worker')")
                    .HasColumnName("user_type")
                    .HasDefaultValueSql("'user'");
            });

            modelBuilder.Entity<WorkListing>(entity =>
            {
                entity.ToTable("work_listing");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.HasIndex(e => e.CategoryId, "work_listing_category_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.RuleFill)
                    .HasColumnName("rule_fill")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.State)
                    .HasColumnType("enum('waiting_approval','approved','rejected')")
                    .HasColumnName("state")
                    .HasDefaultValueSql("'waiting_approval'");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.WorkListings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("work_listing_category_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WorkListings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("work_listing_ibfk_1");
            });

            modelBuilder.Entity<Workcategory>(entity =>
            {
                entity.ToTable("workcategory");

                entity.HasIndex(e => e.ParentId, "workcategory_parent_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("parent_id");

                entity.Property(e => e.PictureUrl)
                    .HasColumnName("pictureUrl")
                    .HasDefaultValueSql("'https://ff1bb85a87a77646636f277e08ace348fb50bfb08550ab9df4abf15-apidata.googleusercontent.com/download/storage/v1/b/hizmetim-image/o/00228-palyaco.webp?jk=ARTXCFFhurD43x1x5N3myqotpOsO0a83IsS130HvXtLsteEqfIWWDpxEsaQ2klhIFZUZWGhcdH-efItW1e_2gfBhEDvE-2IxzBtQ8FrK2R8IPnCupf0TL-4Hezg5nEy8RZ8QALAb4cCJvGhSDtZDBCxw1vwWTltEPQCsA5qxJasUWvK6_73EqSjRAYq_RFUEo7AvcosLWz6yCS0dolHZBvw9GNBh8wOB8REi1vw2eg_n5uK7shM8GZNFn8bG2DGYMpxfj_upfoxONSFjOAqN3EwhivvpsAQOwpj3_gJsDTHIr-UMLJ8tu9jJZWEX-5tPSYnzeJOgde8nETd0OIMhe6k0VDpU_uUMIKc5ZBvyZB-8SnlrnT13PTek8AZwDH-K2vdHeNP5FPsPB0Hk1hcieUrSzdJpfewYFjn_Pawuya7tRdp638xnPj4RRr7lyLv2rLsmAF26LtrILfGdhxXCd9arWRiUxIXRYWQZW_8_NQjCZLyw65qdCs8GDBZTB1qem9uHKPVTvR-aevWTrgANIKD_BWL2zbirrkSLTlYOyPvXLGzwDx6w1HCeWt9VWHbtevu7HFIWPGFJNHEVWydhwqzpccxllmGpZabgd25OlXe8eLaVnszchAh-YDNmPX8GPebvUDwWAAjRBnLJqJpPE9EHhPsZZ2QvfqNOL_oE2MXty6uACLRLZu39SDUPb-gyV0jzmA881RhCwlt3QkuuTUamPrG9Q80mK4SjaQTAbiD3t7KaQ3OxSlJ-1ELvixoerNcGrZKPWjUQMbxx6W_zrBaIjQVvkPzhBwnOoym89KDTDrXUlIbPYsyU6dWlaIp3qbrscrA2zXm8ZoUCNd6cQ1y5T9FuCQTweWoxBgfLJSRGty4or59yHbB6kPeal98VVJBDWrfJJLtNGwyfEwwr_om6aLNbirGWOjdHzqmHwN4TOjgdAmK33DcRjVGHzhhaOwFGwX4lIkhPn4zBa8pjcyrvQ-7OcVjewiuCLILSgF5Qz1usI0Uwwd_2VjtUdMdbAX2r8pvBKEdIWuTxn0PsfCeBywPZrgkhR-zP1nyco341xspRxEjYziEr6-1x-o5wWhg43qURwmRdMD1DU1hD4ZXDJAfsFigYhfMQzqmsWKwCQr-qrQBRMqJc9oLKYpHnvjBuU1cwyuwhxXHkO_Nf7Y_f6_BwLgTJLJSjn0Rr30VjUAIw8ya9A090-hxZfSCQaoSePeW-&isca=1'");

                entity.Property(e => e.RuleTemplate)
                    .HasColumnName("rule_template")
                    .UseCollation("utf8mb4_bin");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("workcategory_parent_id_foreign");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("worker");

                entity.HasIndex(e => e.UserId, "worker_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Adress)
                    .HasColumnType("text")
                    .HasColumnName("adress");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("worker_user_id_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
