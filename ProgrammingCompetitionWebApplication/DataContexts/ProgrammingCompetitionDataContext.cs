using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProgrammingCompetitionWebApplication.DataContexts
{
    public partial class ProgrammingCompetitionDataContext : DbContext
    {
        public ProgrammingCompetitionDataContext() { }

        public ProgrammingCompetitionDataContext(DbContextOptions<ProgrammingCompetitionDataContext> options) : base(options) { }

        public virtual DbSet<ProgrammingTask> ProgrammingTask { get; set; }
        public virtual DbSet<UserSubmission> UserSubmission { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<ProgrammingTask>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__Programm__7C6949B1D3F23BD8");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserSubmission>(entity =>
            {
                entity.HasKey(e => e.SubmissionId)
                    .HasName("PK__Submissi__449EE12593037E1B");

                entity.HasIndex(e => new { e.Nickname, e.TaskId })
                    .HasName("Unique_Nickname_Task_UserSubmissions")
                    .IsUnique();

                entity.Property(e => e.Code).HasColumnType("text");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.SubmissionDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_TaskId");
            });
        }
    }
}
