using Microsoft.EntityFrameworkCore;
using feedback.Models;

namespace feedback.Data
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options)
        {
        }

        public DbSet<Chestionar> Chestionare { get; set; }
        public DbSet<Intrebare> Intrebari { get; set; }
        public DbSet<UserResponse> UserResponse { get; set; }

        // Alte setări și configurări ale contextului
    }
}
