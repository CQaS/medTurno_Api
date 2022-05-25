using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace medTurno_Api.Models
{
    public class Historiaclinica
    {
        [Key]
        [Column ("id")]
        [Display(Name = "Codigo")]
        public int id { get; set; }
        [Required]
        [Display(Name = "Fecha de Alta HC")]
        public DateTime fechainicio { get; set; }
        [Required]
        [Display(Name = "Resumen HC")]
        public String descripcion { get; set; }
        [Required]
        [Display(Name = "Nro. Usuario")]
        public int idUsuario { get; set; }
        [Required]
        [Display(Name = "Fecha de Atencion")]
        public DateTime fechaTurno { get; set; }
        [Required]
        [Display(Name = "Medico o Profecional")]
        public int idDoctor { get; set; }
    }
}