using Microsoft.EntityFrameworkCore;
using project_donation.Models.donor;

namespace project_donation.context
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
