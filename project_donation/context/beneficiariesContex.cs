using Microsoft.EntityFrameworkCore;
using project_donation.Models.Beneficiarie;

namespace project_donation.context.beneficiaries
{
        public class beneficiariesContex : DbContext
        {
            public beneficiariesContex(DbContextOptions<beneficiariesContex> _options)
                : base(_options)
            {

            }
            public DbSet<Beneficiarie> beneficiarie { get; set; }
        }
}
