using Microsoft.EntityFrameworkCore;

namespace VehicalTracking.Common.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
