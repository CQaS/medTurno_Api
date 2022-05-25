using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace medTurno_Api.Models
{
    public class RepositorioTurno : RepositorioBase
    {

        public RepositorioTurno(IConfiguration configuration): base(configuration)
        {

        }
        
        public List<Turno> obtenerHoy()
        {
            var res = new List<Turno>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT t.id, t.start, t.title, d.nombre, u.nombre FROM turnos t JOIN doctor d ON t.idDoctor = d.id JOIN usuario u ON u.id = t.idUsuario WHERE t.estado NOT IN(0, 3, 4) ORDER BY d.nombre";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Turno
                        {
                            Id = reader.GetInt32(0),
                            start = reader.GetString(1),
                            title = reader.GetString(2),
                            doctor = new Doctor
                            {
                                nombre = reader.GetString(3),
                            },
                            usuario = new Usuario
                            {
                                nombre = reader.GetString(4),
                            },
                            
                        };
                        res.Add(e);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public List<Turno> ObtenerPorEstado()
		{
			var t = new List<Turno>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"SELECT t.id, t.idUsuario, u.nombre, t.start, t.idDoctor, d.nombre, t.color, t.estado FROM turnos t JOIN usuario u ON u.id = t.idUsuario JOIN doctor d ON d.id = t.idDoctor WHERE t.estado = 5 OR t.estado = 6 OR t.estado = 2";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())   
                    {
                        var e = new Turno
                        {
                            Id = reader.GetInt32(0),
							idUsuario = reader.GetInt32(1),
                            usuario = new Usuario
                            {
                                nombre = reader.GetString(2),
                            },
							start = reader.GetString(3),
							idDoctor = reader.GetInt32(4),
                            doctor = new Doctor
                            {
                                nombre = reader.GetString(5),
                            },
							color = reader.GetString(6),
							estado = reader.GetInt32(7),                           
                            
						};
                        t.Add(e);
					}
					connection.Close();
				}
			}
			return t;
		}

         public Turno obtenerPorIdTurno(int? id)
        {
            Turno unTurno = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"SELECT t.id, t.fechaSolicitud, t.start, t.color, t.descripcion, t.title, t.estado, u.nombre, d.nombre, p.nombre FROM Turnos t JOIN Usuario u on t.idUsuario = u.id JOIN Doctor d on t.idDoctor = d.id JOIN Prestador p on t.idPrestador = p.id where t.id=@idTurno AND t.estado NOT IN(0, 3, 4)";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add("@idTurno", MySqlDbType.Int32).Value = id;
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if(reader.Read())
                    {
                        unTurno = new Turno
                        {
                            Id = reader.GetInt32(0),
                            fechaSolicitud = reader.GetString(1),
                            start = reader.GetString(2),
                            color = reader.GetString(3),
                            descripcion = reader.GetString(4),
                            title = reader.GetString(5),
                            estado = reader.GetInt32(6),
                            usuario = new Usuario
                            {
                                nombre = reader.GetString(7),
                            },
                            doctor = new Doctor
                            {
                                nombre = reader.GetString(8),
                            },
                            prestador = new Prestador
                            {
                                nombre = reader.GetString(9),
                            },
                            
                        };
                    }
                    connection.Close();                    
                }
            }
            return unTurno;
        }
        
        public int Modificar(int idt, int estadot, string colort)
        {
            var i = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = $"update Turnos SET estado = @estadot, color=@colort where id=@idt";

                using (var command = new MySqlCommand(sql, connection))
                {
                    /* command.Parameters.Add("@idTurno", MySqlDbType.UInt32);
                    command.Parameters["@idTurno"].Value = idt;
                    command.Parameters.Add("@estado", MySqlDbType.UInt32);
                    command.Parameters["@estado"].Value = estadot; */
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@estadot", estadot);
                    command.Parameters.AddWithValue("@colort", colort);
                    command.Parameters.AddWithValue("@idt", idt);
                    
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }
    }
}
    

