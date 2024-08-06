using Microsoft.EntityFrameworkCore;
using myRestApiApp.Models;
namespace myRestApiApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Module> Modules { get; set; }
        public DbSet<StudySession> StudySessions { get; set; }

        // relationship between module and sessions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudySession>()
                .HasOne(ss => ss.Module)
                .WithMany()
                .HasForeignKey(ss => ss.ModuleId);
        }
    }
}
