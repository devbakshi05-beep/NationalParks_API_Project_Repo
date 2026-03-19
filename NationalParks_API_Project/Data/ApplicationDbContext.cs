using Microsoft.EntityFrameworkCore;
using NationalParks_API_Project.Models;

namespace NationalParks_API_Project.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            :base(options)
        {
            
        }
        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<User > Users { get; set; }
    }
}
