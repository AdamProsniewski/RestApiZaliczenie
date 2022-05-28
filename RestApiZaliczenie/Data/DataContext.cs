using Microsoft.EntityFrameworkCore;

namespace RestApiZaliczenie.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){

        }
    }
}
