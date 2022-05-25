using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
 
namespace medTurno_Api.Models
{
    public class RepositorioHistoriaclinica: RepositorioBase
    {

        public RepositorioHistoriaclinica(IConfiguration configuration): base(configuration)
        {

        }
        
        public List<Historiaclinica> obtener()
        {
            var res = new List<Historiaclinica>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT {nameof(Historiaclinica.id)}, {nameof(Historiaclinica.fechainicio)}, {nameof(Historiaclinica.descripcion)}, {nameof(Historiaclinica.idUsuario)}, {nameof(Historiaclinica.fechaTurno)}, {nameof(Historiaclinica.idDoctor)} FROM Historiaclinica WHERE estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Historiaclinica
                        {
                            id = reader.GetInt32(0),
                            fechainicio = reader.GetDateTime(1),
                            descripcion = reader.GetString(2),
                            idUsuario = reader.GetInt32(3),
                            fechaTurno = reader.GetDateTime(4),
                            idDoctor = reader.GetInt32(5),
                            /* Duenio = new Propietario
                            {
                                
                                Id = reader.GetInt32(6),
                                Nombre = reader.GetString(7),
                            } */
                        };
                        res.Add(e);
                    }
                    connection.Close();
                }

            }
            return res;
        }

        /* public IList<Historiaclinica> BuscarHistoriaclinicasDisponibles(BuscarPorFecha f)
		{
			var res = new List<Historiaclinica>();

			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = "SELECT i.id_Inmu, i.direccion_in, i.Uso, i.Tipo, i.Ambientes, i.precio, i.id_propietario, p.Nombre FROM Historiaclinica i INNER JOIN Propietarios p ON i.id_Propietario = p.id WHERE (SELECT COUNT(c.id) AS contID FROM Contrato c WHERE c.id_inmueble = i.id_Inmu AND ((c.fe_ini BETWEEN @inicio AND @fin) OR (c.fe_fin BETWEEN @inicio AND @fin) OR (c.fe_ini < @inicio AND c.fe_fin > @fin))) = 0 AND i.estado = 1";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@inicio", MySqlDbType.Date).Value = f.FechaInicio;
					command.Parameters.Add("@fin", MySqlDbType.Date).Value = f.FechaFin;
					command.CommandType = System.Data.CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						var e = new Historiaclinica
                        {
                            Id_inmu = reader.GetInt32(0),
                            Direccion_in = reader.GetString(1),
                            Uso = reader.GetString(2),
                            Tipo = reader.GetString(3),
                            ambientes = reader.GetInt32(4),
                            precio = reader.GetInt32(5),
                            id_propietario = reader.GetInt32(6),
                            Duenio = new Propietario
                            {
                                
                                Id = reader.GetInt32(6),
                                Nombre = reader.GetString(7),
                            }
                        };
                        res.Add(e);
					}
					connection.Close();
				}
			}
		return res;

		} */

        /* public List<Historiaclinica> obtenerPorPropietario(int id)
        {
            var res = new List<Historiaclinica>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT {nameof(Historiaclinica.Id_inmu)}, {nameof(Historiaclinica.Direccion_in)}, {nameof(Historiaclinica.Uso)}, {nameof(Historiaclinica.Tipo)}, {nameof(Historiaclinica.ambientes)}, {nameof(Historiaclinica.precio)}, {nameof(Historiaclinica.foto)} FROM Historiaclinica WHERE id_propietario = @id AND estado = 1";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Historiaclinica
                        {
                            Id_inmu = reader.GetInt32(0),
                            Direccion_in = reader.GetString(1),
                            Uso = reader.GetString(2),
                            Tipo = reader.GetString(3),
                            ambientes = reader.GetInt32(4),
                            precio = reader.GetInt32(5),
                            foto = reader.GetString(6),
                        };
                        res.Add(e);
                    }
                    connection.Close();
                }

            }
            return res;
        } */

        public Historiaclinica Buscar(int id)
        {
            Historiaclinica HC;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT {nameof(Historiaclinica.id)}, {nameof(Historiaclinica.fechainicio)}, {nameof(Historiaclinica.descripcion)}, {nameof(Historiaclinica.idUsuario)}, {nameof(Historiaclinica.fechaTurno)}, {nameof(Historiaclinica.idDoctor)} FROM Historiaclinica where id = @idHC";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@idHC", MySqlDbType.Int32).Value = id;
                    connection.Open();
                    MySqlDataReader res = command.ExecuteReader();
                    HC = new Historiaclinica();
                    if(res.Read())
                    {
                        HC.id = int.Parse(res["id"].ToString());
                        HC.fechainicio = DateTime.Parse(res["fechaInicio"].ToString());
                        HC.descripcion = res["descripcion"].ToString();
                        HC.idUsuario = int.Parse(res["idUsuario"].ToString());
                        HC.fechaTurno = DateTime.Parse(res["fechaTurno"].ToString());;
                        HC.idDoctor = int.Parse(res["idDoctor"].ToString());
                    }
                    connection.Close();                                       
                }
            }
            return HC;
        }

        public int Alta(Historiaclinica e)
        {
            var res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Historiaclinica (descripcion, idUsuario, fechaTurno, idDoctor) VALUES (@descripcion, @idUsuario, @fechaTurno, @idDoctor)";
                
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
		            command.Parameters.AddWithValue("@descripcion", e.descripcion);
                    command.Parameters.AddWithValue("@idUsuario", e.idUsuario);
                    command.Parameters.AddWithValue("@fechaTurno", e.fechaTurno);
                    command.Parameters.AddWithValue("@idDoctor", e.idDoctor);
                    connection.Open();
                    command.ExecuteScalar();
                    connection.Close();
                }

                string sql_ID = $"SELECT MAX(id) AS idHC FROM Historiaclinica";                
                using (var command = new MySqlCommand(sql_ID, connection))
                {
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int Editar(Historiaclinica e)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Historiaclinica set descripcion=@descripcion, idUsuario=@idUsuario, fechaTurno=@fechaTurno, idDoctor=@idDoctor where id = @id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@descripcion", MySqlDbType.VarChar);
                    command.Parameters["@descripcion"].Value = e.descripcion;
                    command.Parameters.Add("@idUsuario", MySqlDbType.VarChar);
                    command.Parameters["@idUsuario"].Value = e.idUsuario;
                    command.Parameters.Add("@fechaTurno", MySqlDbType.VarChar);
                    command.Parameters["@fechaTurno"].Value = e.fechaTurno;
                    command.Parameters.Add("@idDoctor", MySqlDbType.VarChar);
                    command.Parameters["@idDoctor"].Value = e.idDoctor;
                    command.Parameters.Add("@id", MySqlDbType.VarChar);
                    command.Parameters["@id"].Value = e.id;
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }

        /* public int Borrar(int idHC)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Historiaclinica set estado='0' where idHC=@idHC";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@idHC", MySqlDbType.UInt32);
                    command.Parameters["@idHC"].Value = idHC;
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        } */

    }
}
    

