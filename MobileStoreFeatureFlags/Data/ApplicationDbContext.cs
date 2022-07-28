using Microsoft.EntityFrameworkCore;

namespace MobileStoreFeatureFlags.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}