using project_donation.Models.Beneficiarie;
using System.Data.SqlClient;
namespace project_donation.services
{
    public class BeneficiariesAdoService
    {
        private readonly string _connectionString;
        public readonly IConfiguration _Configuration;
        public readonly object reader;

        public BeneficiariesAdoService(IConfiguration Configuration)
        {
            _Configuration = Configuration;
            _connectionString = _Configuration.GetConnectionString("cadenaSQL");
        }
        public void Add(Beneficiarie _Beneficiarie)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var klk = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand(" insert into Beneficiaries ( name_Benefi, phone_Benefi )  values ( @name_Benefi, @phone_Benefi )", connection);
                        command.Parameters.AddWithValue(" @name_Benefi", _Beneficiarie.name_Benefi);
                        command.Parameters.AddWithValue(" @phone_Benefi", _Beneficiarie.phone_Benefi);

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
                var command = new SqlCommand("delete from Beneficiaries where id_Benefi = @id_Benefi ", connection);
                command.Parameters.AddWithValue("id_Benefi", id);
                connection.Open();
                command.ExecuteNonQuery();
                command.Clone();
        }
        public Beneficiarie GetBYId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("select * from Beneficiaries where id_Benefi = @id_Benefi ", connection);
                command.Parameters.AddWithValue("id_Benefi", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                            return new Beneficiarie
                            {
                                id_Benefi = (int)reader["id_Benefi"],
                                name_Benefi = reader["name_Benefi"].ToString(),
                                phone_Benefi = reader["phone_Benefi"].ToString(),
                               
                            };
                    }
                }
            }
            return null;
        }


            public List<Beneficiarie> Getall()
            {
                var Beneficiaries = new List<Beneficiarie>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("select * from Beneficiaries", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                       while (reader.Read())
                       {
                           Beneficiaries.Add(new Beneficiarie
                           {
                             id_Benefi = (int)reader["id_Benefi"],
                             name_Benefi = reader["name_Benefi"].ToString(),
                             phone_Benefi = reader["phone_Benefi"].ToString(),
                           });
                       }
                    }
                }
                return Beneficiaries;
            }
            public void Update(Beneficiarie _Beneficiarie)
            {
                using (var connection = new SqlConnection(_connectionString))
                {               
                    var command = new SqlCommand("Update Beneficiaries set id_Benefi = @id_Benefi, name_Benefi = @name_Benefi, phone_Benefi = @phone_Benefi  where id_Benefi = @id_Benefi ", connection);
                    command.Parameters.AddWithValue(" @id_Benefi", _Beneficiarie.id_Benefi);
                    command.Parameters.AddWithValue(" @name_Benefi", _Beneficiarie.name_Benefi);
                    command.Parameters.AddWithValue(" @phone_Benefi", _Beneficiarie.phone_Benefi);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
    }
}
