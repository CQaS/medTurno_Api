using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
 
namespace medTurno_Api.Models
{
    public class RepositorioDoctor : RepositorioBase
    {

        public RepositorioDoctor(IConfiguration configuration): base(configuration)
        {

        }
        
        public List<Doctor> obtener()
        {
            var res = new List<Doctor>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT d.id, d.nombre, d.matricula, d.horarioatencion, e.tipo, e.especialidad FROM Doctor d JOIN especialidad e on d.idEspecialidad = e.id where d.estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Doctor
                        {
                            id = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            matricula = reader.GetString(2),
                            horarioatencion = reader.GetString(3),
                            especialidad = new Especialidad
                            {
                                tipo = reader.GetString(4),
                                especialidad = reader.GetString(5),
                            }
                        };
                        res.Add(e);
                    }
                    connection.Close();
                }

            }
            return res;
        }

        public Doctor Buscar(int id)
        {
            Doctor doc; Especialidad especialidad;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT id, nombre, matricula, horarioatencion, idEspecialidad FROM Doctor WHERE id = @id AND estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    connection.Open();
                    MySqlDataReader res = command.ExecuteReader();
                    doc = new Doctor();
                    especialidad = new Especialidad();
                    if(res.Read())
                    {
                        doc.id = int.Parse(res["id"].ToString());
                        doc.nombre = res["nombre"].ToString();
                        doc.matricula = res["matricula"].ToString();
                        doc.horarioatencion = res["horarioatencion"].ToString();
                        doc.idEspecialidad = int.Parse(res["idEspecialidad"].ToString());                        
                    }
                    connection.Close();                                       
                }
            }
            return doc;
        }


        public int Alta(Doctor e)
        {
            var res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Doctor (nombre, matricula, horarioatencion, idEspecialidad) VALUES (@nombre, @matricula, @horarioatencion, @idEspecialidad)";
                
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
					command.Parameters.AddWithValue("@nombre", e.nombre);
                    command.Parameters.AddWithValue("@matricula", e.matricula);
                    command.Parameters.AddWithValue("@horarioatencion", e.horarioatencion);
                    command.Parameters.AddWithValue("@idEspecialidad", e.idEspecialidad);
                    
                    connection.Open();
                    command.ExecuteScalar();
                    connection.Close();
                }

                string sql_ID = $"SELECT MAX(id) AS idUltimo FROM Doctor";                
                using (var command = new MySqlCommand(sql_ID, connection))
                {
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int Editar(Doctor e)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Doctor set nombre=@nombre, matricula=@matricula, horarioatencion=@horarioatencion, idEspecialidad=@idEspecialidad where id=@id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", e.nombre);
					command.Parameters.AddWithValue("@matricula", e.matricula);
                    command.Parameters.AddWithValue("@horarioatencion", e.horarioatencion);
                    command.Parameters.AddWithValue("@idEspecialidad", e.idEspecialidad);
					command.Parameters.AddWithValue("@id", e.id);

                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }

        public int Borrar(int idDoc)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Doctor set estado='0' where id=@id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.UInt32);
                    command.Parameters["@id"].Value = idDoc;

                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            return i;
        }
    }
}
    
