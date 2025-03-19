using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_donation.Models.Beneficiarie
{
    [Table("Beneficiaries")]
    public class Beneficiarie
    {
        [Key]
        public int id_Benefi { get; set; } 
        public string name_Benefi { get; set; }
        public string phone_Benefi { get; set; }
    }
}
