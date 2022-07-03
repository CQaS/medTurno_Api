using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
 
namespace medTurno_Api.Models
{
    public class RepositorioEspecialidad : RepositorioBase
    {

        public RepositorioEspecialidad(IConfiguration configuration): base(configuration)
        {

        }
        
        public List<Especialidad> obtener()
        {
            var res = new List<Especialidad>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT id, tipo, especialidad FROM especialidad where estado = 1 ORDER BY tipo";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Especialidad
                        {
                            id = reader.GetInt32(0),
                            tipo = reader.GetString(1),
                            especialidad = reader.GetString(2),
                        };
                        res.Add(e);
                    }
                    connection.Close();
                }

            }
            return res;
        }
        public Especialidad Buscar(int id)
        {
            Especialidad esp;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT e.id, e.tipo, e.especialidad FROM especialidad e WHERE e.id = @id AND estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    connection.Open();
                    MySqlDataReader res = command.ExecuteReader();
                    esp = new Especialidad();
                    if(res.Read())
                    {
                        esp.id = int.Parse(res["id"].ToString());
                        esp.tipo = res["tipo"].ToString();
                        esp.especialidad = res["especialidad"].ToString();
                    }
                    connection.Close();                                       
                }
            }
            return esp;
        }


        public int Alta(Especialidad e)
        {
            var res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Especialidad (tipo, especialidad) VALUES (@tipo, @especialidad)";
                
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
					command.Parameters.AddWithValue("@tipo", e.tipo);
                    command.Parameters.AddWithValue("@especialidad", e.especialidad);
                    
                    connection.Open();
                    command.ExecuteScalar();
                    connection.Close();
                }

                string sql_ID = $"SELECT MAX(id) AS idUltimo FROM Especialidad";                
                using (var command = new MySqlCommand(sql_ID, connection))
                {
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int Editar(Especialidad e)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Especialidad set tipo=@tipo, especialidad=@especialidad where id=@id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tipo", e.tipo);
					command.Parameters.AddWithValue("@especialidad", e.especialidad);
					command.Parameters.AddWithValue("@id", e.id);

                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }

        public int Verificar(int idEspecialidad)
        {
            var ccontar = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql_ID = $"SELECT COUNT(d.id) FROM especialidad e JOIN doctor d ON e.id = d.idEspecialidad WHERE e.id = @idEspecialidad AND d.estado NOT IN(0)";                
                using (var command = new MySqlCommand(sql_ID, connection))
                {
                    command.Parameters.Add("@idEspecialidad", MySqlDbType.UInt32);
                    command.Parameters["@idEspecialidad"].Value = idEspecialidad;
                    connection.Open();
                    ccontar = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return ccontar;
        }


        public int Borrar(int idE)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Especialidad set estado='0' where id=@id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.UInt32);
                    command.Parameters["@id"].Value = idE;

                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }
    }
}
    
