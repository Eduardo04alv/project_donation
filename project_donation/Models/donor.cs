using System.ComponentModel.DataAnnotations;

namespace project_donation.Models.donor
{

    public class Donor
    {
        [Key]
        public int id_donor { get; set; }
        public string name_donor { get; set; }
        public string email_donor { get; set; }
        public string phone_donor { get; set; }
    }
}
