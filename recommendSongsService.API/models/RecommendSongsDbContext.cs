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
    }
}