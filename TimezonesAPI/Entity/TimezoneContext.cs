using Microsoft.EntityFrameworkCore;

namespace TimezonesAPI.Entity
{
    public class TimezoneContext : DbContext
    {
        public TimezoneContext(DbContextOptions<TimezoneContext> options) : base(options) { }
        public DbSet<Timezone> _timezones { get; set; }

    }
}
