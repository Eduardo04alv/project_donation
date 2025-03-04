using project_donation.Models.donor;
using System.Data.OleDb;
namespace project_donation.services
{
    public class DonationAdoService
    {
        private readonly string connectionString;

        public DonationAdoService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }
        public void AddDonor(donor donor)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "INSERT INTO donor (id_donor, name_donor, email_donor, phone_donor) VALUES (@id_donor, @name_donor, @email_donor, @phone_donor)";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_donor", donor.id_donor);
                cmd.Parameters.AddWithValue("@name_donor", donor.name_donor);
                cmd.Parameters.AddWithValue("@email_donor", donor.email_donor);
                cmd.Parameters.AddWithValue("@phone_donor", donor.phone_donor);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<donor> GetDonors()
        {
            List<donor> donors = new List<donor>();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM donor";
                OleDbCommand cmd = new OleDbCommand(query, conn);

                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    donors.Add(new donor
                    {
                        id_donor = Convert.ToInt32(reader["id_donor"]),
                        name_donor = reader["name_donor"].ToString(),
                        email_donor = reader["email_donor"].ToString(),
                        phone_donor = reader["phone_donor"].ToString()
                    });
                }
            }
            return donors;
        }
    }
}

