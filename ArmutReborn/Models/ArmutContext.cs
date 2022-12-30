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
                optionsBuilder.UseMySql("server=3.127.53.229;uid=Eray;pwd=armut;database=Armut", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.11-mariadb"));
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
