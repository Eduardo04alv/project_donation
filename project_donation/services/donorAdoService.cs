using project_donation.Models.donor;
using System.Data.SqlClient;
namespace project_donation.services
{
    public class DonationAdoService
    {

        private readonly string _connectionString;
        public readonly IConfiguration _Configuration;
        public readonly object reader;

        public DonationAdoService(IConfiguration Configuration)
        {
            _Configuration = Configuration;
            _connectionString = _Configuration.GetConnectionString("cadenaSQL");
        }
        public void Add(Donor _donor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var klk = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand(" insert into donor (id_donor, name_donor, email_donor , phone_donor )  values (@id_donor, @name_donor, @email_donor , @phone_donor )", connection);
                        command.Parameters.AddWithValue("@id_donor", _donor.id_donor);
                        command.Parameters.AddWithValue(" @name_donor", _donor.name_donor);
                        command.Parameters.AddWithValue(" @email_donor", _donor.email_donor);
                        command.Parameters.AddWithValue("@phone_donor", _donor.phone_donor);

                        command.ExecuteNonQuery();
                        klk.Commit();
                    }
                    catch (Exception)
                    {
                        klk.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void Delate(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("delete from donor where id_donor = @id_donor ", connection);
            command.Parameters.AddWithValue("id_donor", id);
            connection.Open();
            command.ExecuteNonQuery();
            command.Clone();
        }

        public Donor GetBYId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("select * from donor where id_donor = @id_donor ", connection);
                command.Parameters.AddWithValue("id_donor", id);
                connection.Open();
                using (var reader = command.ExecuteReader()) 
                { 
                    if(reader.Read())
                    {

                        return new Donor
                        {
                            id_donor = (int)reader["id_donor"],
                            name_donor = reader["name_donor"].ToString(),
                            email_donor = reader["email_donor"].ToString(),
                            phone_donor = reader["phone_donor"].ToString()
                        };
                    }
                }
            }
            return null;
        }


        public List<Donor> Getall()
        {
            var donors = new List<Donor>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("select * from donor", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        donors.Add(new Donor
                        {
                            id_donor = (int)reader["id_donor"],
                            name_donor = reader["name_donor"].ToString(),
                            email_donor = reader["email_donor"].ToString(),
                            phone_donor = reader["phone_donor"].ToString()
                        });
                    }
                }
            }
            return donors;
        }
        public void Update(Donor _donor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("Update donor set id_donor = @id_donor, name_donor = @name_donor, email_donor = @email_donor,  phone_donor = @phone_donor  where id_donor = @id_donor ", connection);
                command.Parameters.AddWithValue("@id_donor", _donor.id_donor);
                command.Parameters.AddWithValue(" @name_donor", _donor.name_donor);
                command.Parameters.AddWithValue(" @email_donor", _donor.email_donor);
                command.Parameters.AddWithValue("@phone_donor", _donor.phone_donor);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
