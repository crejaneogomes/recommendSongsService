using Microsoft.EntityFrameworkCore;
namespace recommendSongsService.Model
{
    public class RecommendSongsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        public RecommendSongsDbContext(DbContextOptions<RecommendSongsDbContext> options) :
            base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        // modelBuilder.Entity<User>().HasData(
        //         new User { Id=-2, Name = "CROG" },
        //         new User { Id=-1, Name = "Docker Magazine" }
        //     );
        // }
    }
}