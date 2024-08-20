using Microsoft.EntityFrameworkCore;
using Notes.Models;
using WebApplication1.Models;

namespace Data.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<NoteLabel> NoteLabels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).HasMaxLength(1000);

                // Defining the relationship between Note and User
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Notes)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete
            });

            modelBuilder.Entity<Label>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<NoteLabel>(entity =>
            {
                // Composite Key
                entity.HasKey(nl => new { nl.NoteId, nl.LabelId });

                // Defining the relationship between NoteLabel and Note
               // entity.HasOne(nl => nl.Note)
                     // .WithMany(n => n.NoteLabels)
                     // .HasForeignKey(nl => nl.NoteId);

                // Defining the relationship between NoteLabel and Label
                entity.HasOne(nl => nl.Label)
                      .WithMany(l => l.NoteLabels)
                      .HasForeignKey(nl => nl.LabelId);
            });
        }
    }
    }
