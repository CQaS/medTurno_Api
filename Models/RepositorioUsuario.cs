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
    public class RepositorioUsuario : RepositorioBase
	{
		public RepositorioUsuario(IConfiguration configuration) : base(configuration)
		{

		}

        public int Alta(Usuario e, int idDireccion)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Usuario (dni, fecNac, Mail, nombre, telefono, avatar, password, pregunta, rol, idprestador, idDireccion) VALUES (@dni, @fecNac, @mail, @nombre, @telefono, @avatar, @password, @pregunta, @rol, @idprestador, @iddireccion)";

				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@dni", e.dni);
					command.Parameters.AddWithValue("@fecNac", e.fecNac);
					command.Parameters.AddWithValue("@mail", e.Mail);
					command.Parameters.AddWithValue("@nombre", e.nombre);
					command.Parameters.AddWithValue("@telefono", e.telefono);
					command.Parameters.AddWithValue("@avatar", e.Avatar);					
					command.Parameters.AddWithValue("@password", e.password);
					command.Parameters.AddWithValue("@pregunta", e.pregunta);
					command.Parameters.AddWithValue("@rol", e.Rol);
					command.Parameters.AddWithValue("@idprestador", e.idPrestador);
					command.Parameters.AddWithValue("@iddireccion", idDireccion);

					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					e.Id = res;
					connection.Close();
				}

                string sql_ID = $"SELECT MAX(id) AS id FROM Usuario";
                
                using (var command = new MySqlCommand(sql_ID, connection))
                {
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
			}
			return res;
		}

		public int Baja(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"UPDATE usuario SET estado = 0 WHERE id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public int session(string hashLog)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"UPDATE usuario SET password=@hashLog WHERE mail = 'hashLog@mail.com'";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@hashLog", hashLog);

					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public int Modificacion(Usuario e)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"UPDATE usuario SET dni=@dni, fecNac=@fecNac, mail=@mail, nombre=@nombre, telefono=@telefono, avatar=@avatar, password=@password, pregunta=@pregunta, rol=@rol, idprestador=@idprestador WHERE id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@dni", e.dni);
					command.Parameters.AddWithValue("@fecNac", e.fecNac);
					command.Parameters.AddWithValue("@mail", e.Mail);
					command.Parameters.AddWithValue("@nombre", e.nombre);
					command.Parameters.AddWithValue("@telefono", e.telefono);
					command.Parameters.AddWithValue("@avatar", e.Avatar);					
					command.Parameters.AddWithValue("@password", e.password);
					command.Parameters.AddWithValue("@pregunta", e.pregunta);
					command.Parameters.AddWithValue("@rol", e.Rol);
					command.Parameters.AddWithValue("@fecAlta", e.fecAlta);
					command.Parameters.AddWithValue("@idprestador", e.idPrestador);

					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public int Modifica(Usuario e)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"UPDATE usuario SET dni=@dni, fecNac=@fecNac, mail=@mail, nombre=@nombre, telefono=@telefono, password=@password, pregunta=@pregunta, rol=@rol, idprestador=@idprestador, idDireccion=@idDireccion WHERE id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@dni", e.dni);
					command.Parameters.AddWithValue("@fecNac", e.fecNac);
					command.Parameters.AddWithValue("@mail", e.Mail);
					command.Parameters.AddWithValue("@nombre", e.nombre);
					command.Parameters.AddWithValue("@telefono", e.telefono);					
					command.Parameters.AddWithValue("@password", e.password);
					command.Parameters.AddWithValue("@pregunta", e.pregunta);
					command.Parameters.AddWithValue("@rol", e.Rol);
					command.Parameters.AddWithValue("@idprestador", e.idPrestador);
					command.Parameters.AddWithValue("@idDireccion", e.idDireccion);
					command.Parameters.AddWithValue("@id", e.Id);

					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public int ModificarPass(string m, string p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"UPDATE Usuario SET password=@password WHERE mail = @mail";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
			
					command.Parameters.AddWithValue("@password", p);
					command.Parameters.AddWithValue("@mail", m);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Usuario> ObtenerTodos()
		{
			var res = new List<Usuario>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"SELECT id, dni, fecNac, Mail, nombre, telefono, avatar, password, pregunta, rol, idprestador FROM usuario WHERE rol NOT IN(1) AND estado = 1";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Usuario e = new Usuario
						{
							Id = reader.GetInt32(0),
							dni = reader.GetInt32(1),
							fecNac = reader.GetDateTime(2),							
                            Mail = reader.GetString(3),
							nombre = reader.GetString(4),
							telefono = reader.GetInt32(5),
							Avatar = reader.GetString(6),
							password = reader.GetString(7),
							pregunta = reader.GetString(8),
							Rol = reader.GetInt32(9),
							idPrestador = reader.GetInt32(10),
						};
						res.Add(e);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Usuario ObtenerPorId(int id)
		{
			Usuario e = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"SELECT id, dni, fecNac, Mail, nombre, telefono, avatar, password, pregunta, rol, idprestador, idDireccion FROM usuario WHERE id=@id AND estado = 1";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						e = new Usuario
						{
							Id = reader.GetInt32(0),
							dni = reader.GetInt32(1),
							fecNac = reader.GetDateTime(2),							
                            Mail = reader.GetString(3),
							nombre = reader.GetString(4),
							telefono = reader.GetInt32(5),
							Avatar = reader.GetString(6),
							password = reader.GetString(7),
							pregunta = reader.GetString(8),
							Rol = reader.GetInt32(9),
							idPrestador = reader.GetInt32(10),
							idDireccion = reader.GetInt32(11),
						};
					}
					connection.Close();
				}
			}
			return e;
		}

		public Usuario ObtenerPorEmail(string mail)
		{
			Usuario e = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"SELECT id, dni, fecNac, Mail, nombre, telefono, avatar, password, pregunta, rol, idprestador FROM usuario WHERE mail=@mail";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = System.Data.CommandType.Text;
					command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = mail;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						e = new Usuario
						{
							Id = reader.GetInt32(0),
							dni = reader.GetInt32(1),
							fecNac = reader.GetDateTime(2),							
                            Mail = reader.GetString(3),
							nombre = reader.GetString(4),
							telefono = reader.GetInt32(5),
							Avatar = reader.GetString(6),
							password = reader.GetString(7),
							pregunta = reader.GetString(8),
							Rol = reader.GetInt32(9),
							idPrestador = reader.GetInt32(10),
						};
					}
					connection.Close();
				}
			}
			return e;
		}

		public Usuario ObtenerPorDni(int dni)
		{
			Usuario e = null;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"SELECT id, dni, fecNac, Mail, nombre, telefono, avatar, password, pregunta, rol, idprestador FROM usuario WHERE dni=@dni";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = System.Data.CommandType.Text;
					command.Parameters.Add("@dni", MySqlDbType.VarChar).Value = dni;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						e = new Usuario
						{
							Id = reader.GetInt32(0),
							dni = reader.GetInt32(1),
							fecNac = reader.GetDateTime(2),							
                            Mail = reader.GetString(3),
							nombre = reader.GetString(4),
							telefono = reader.GetInt32(5),
							Avatar = reader.GetString(6),
							password = reader.GetString(7),
							pregunta = reader.GetString(8),
							Rol = reader.GetInt32(9),
							idPrestador = reader.GetInt32(10),
						};
					}
					connection.Close();
				}
			}
			return e;
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
    

