using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
 
namespace medTurno_Api.Models
{
    public class RepositorioPrestador: RepositorioBase
    {

        public RepositorioPrestador(IConfiguration configuration): base(configuration)
        {

        }
        
        public List<Prestador> obtener()
        {
            var res = new List<Prestador>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT id, nombre, direccion, telefono FROM Prestador WHERE estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var p = new Prestador
                        {
                            Id = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            direccion = reader.GetString(2),
                            telefono = reader.GetInt32(3)
                        };
                        res.Add(p);
                    }
                    connection.Close();
                }

            }
            return res;
        }

        public Prestador Buscar(int id)
        {
            Prestador p = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT id, nombre, direccion, telefono FROM Prestador WHERE id=@id AND estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    p = new Prestador();
                    if(reader.Read())
                    {
                        p.Id = int.Parse(reader["id"].ToString());
                        p.nombre = reader["nombre"].ToString();
                        p.direccion = reader["direccion"].ToString();
                        p.telefono = int.Parse(reader["telefono"].ToString());
                    }
                    connection.Close();                                       
                }
            }
            return p;
        }

        public int Alta(Prestador e)
        {
            
            var res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Prestador (nombre, direccion, telefono) VALUES (@nombre, @direccion, @telefono)";
                
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
					command.Parameters.AddWithValue("@nombre", e.nombre);
					command.Parameters.AddWithValue("@direccion", e.direccion);
					command.Parameters.AddWithValue("@telefono", e.telefono);

                    connection.Open();
                    command.ExecuteScalar();
                    connection.Close();
                }

                string sql_ID = $"SELECT MAX(id) AS idUltimo FROM Prestador";                
                using (var command = new MySqlCommand(sql_ID, connection))
                {
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int Editar(Prestador e)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Prestadors set nombre=@nombre,  direccion=@direccion, telefono=@telefono WHERE id = @id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre",e.nombre);
					command.Parameters.AddWithValue("@direccion", e.direccion);
					command.Parameters.AddWithValue("@telefono", e.telefono);
					command.Parameters.AddWithValue("@id", e.Id);
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }

        public int Borrar(int idPrestador)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Prestador set estado='0' where id=@id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.UInt32);
                    command.Parameters["@id"].Value = idPrestador;
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }
    }
}
    

