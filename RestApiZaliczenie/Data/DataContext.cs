using Microsoft.EntityFrameworkCore;

namespace RestApiZaliczenie.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){

        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<SavedActivities> SavedActivities { get; set; }
    }
}
