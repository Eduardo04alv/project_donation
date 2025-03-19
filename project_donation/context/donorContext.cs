using Microsoft.EntityFrameworkCore;
using project_donation.Models.Donor;

namespace project_donation.context.donor
{
    public class donorContext : DbContext
    {
        public donorContext(DbContextOptions<donorContext> options)
            : base(options)
        { 

        }
      public  DbSet<Donor> donor { get; set; }
    }
}
