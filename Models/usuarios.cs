using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace medTurno_Api.Models
{
    public enum enRoles
	{
		Administrador = 1,
		Empleado = 2,
		Paciente = 3,
		Doctor = 4,		
	}
	
	public class Usuario
    {
		[Key]
		[Display(Name = "Código")]
		public int Id { get; set; }
		[Required]
		[RegularExpression(@"[0-9]+")]
		public int dni { get; set;}
		[Required]
		[Display(Name = "Fecha de Nacimiento")]
		public DateTime fecNac { get; set;}
		[Required, DataType(DataType.EmailAddress)]
		public string Mail { get; set; }
		[Required]
		public string nombre { get; set; }
		[Required]
		public int telefono { get; set; }
        public string Avatar { get; set; }

		[Display(Name = "Avatar")]
		[NotMapped]
        public IFormFile AvatarFile{ get; set; }
			
		[DataType(DataType.Password)]
		public string password { get; set; }

		[Display(Name = "Respuesta secreta")]
		public string pregunta { get; set; }
		[Display(Name = "Rol de Usuario")]
		public int Rol { get; set; }

		[NotMapped]
		public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";
		public static IDictionary<int, string> ObtenerRoles()
		{
			SortedDictionary<int, string> roles = new SortedDictionary<int, string>();
			Type tipoEnumRol = typeof(enRoles);
			foreach (var valor in Enum.GetValues(tipoEnumRol))
			{
				roles.Add((int)valor, Enum.GetName(tipoEnumRol, valor));
			}
			return roles;
		}
		[Display(Name = "Fecha del Alta")]
		public DateTime fecAlta { get; set; }
		public int estado { get; set; }

		[Display(Name = "Prestador n:")]
		public int idPrestador { get; set; }

		[ForeignKey(nameof(idPrestador))]
		public Prestador prestador { get; set; }

		[Display(Name = "Direccion")]
		public int idDireccion { get; set; }
		
		[ForeignKey(nameof(idDireccion))]
		public Direccion direccion { get; set; }
	}
}