using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
 
namespace medTurno_Api.Models
{
    public class RepositorioDireccion : RepositorioBase
    {

        public RepositorioDireccion(IConfiguration configuration): base(configuration)
        {

        }
        
        public List<Direccion> obtener()
        {
            var res = new List<Direccion>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT id, calle, numero, ciudad FROM Direccion where estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Direccion
                        {
                            id = reader.GetInt32(0),
                            calle = reader.GetString(1),
                            numero = reader.GetInt32(2),
                            ciudad = reader.GetString(3),
                        };
                        res.Add(e);
                    }
                    connection.Close();
                }

            }
            return res;
        }

        public Direccion Buscar(int id)
        {
            Direccion direc;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT id, calle, numero, ciudad FROM Direccion WHERE id = @id AND estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    connection.Open();
                    MySqlDataReader res = command.ExecuteReader();
                    direc = new Direccion();
                    if(res.Read())
                    {
                        direc.id = int.Parse(res["id"].ToString());
                        direc.calle = res["calle"].ToString();
                        direc.numero = int.Parse(res["numero"].ToString());
                        direc.ciudad = res["ciudad"].ToString();
                    }
                    connection.Close();                                       
                }
            }
            return direc;
        }


        public int Alta(string calle, int numero, string ciudad)
        {
            var res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Direccion (calle, numero, ciudad) VALUES (@calle, @numero, @ciudad)";
                
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
					command.Parameters.AddWithValue("@calle", calle);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.Parameters.AddWithValue("@ciudad", ciudad);
                    
                    connection.Open();
                    command.ExecuteScalar();
                    connection.Close();
                }

                string sql_ID = $"SELECT MAX(id) AS idUltimo FROM Direccion";                
                using (var command = new MySqlCommand(sql_ID, connection))
                {
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int Editar(Direccion e)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Direccion set calle=@calle, numero=@numero, ciudad=@ciudad where id=@id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@calle", e.calle);
					command.Parameters.AddWithValue("@numero", e.numero);
                    command.Parameters.AddWithValue("@ciudad", e.ciudad);
					command.Parameters.AddWithValue("@id", e.id);

                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }

        public int Borrar(int idD)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Direccion set estado ='0' where id=@id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.UInt32);
                    command.Parameters["@id"].Value = idD;

                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            return i;
        }
    }
}
    
