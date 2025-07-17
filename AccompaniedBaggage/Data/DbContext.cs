using Microsoft.EntityFrameworkCore;

namespace SezApi.Data
{
    public class AccompaniedBaggageDbContext : DbContext
    {
        public AccompaniedBaggageDbContext(DbContextOptions<AccompaniedBaggageDbContext> options)
            : base(options)
        {
        }
    }
}

