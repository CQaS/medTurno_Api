using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace medTurno_Api.Models
{
    public class Prestador
    {
        [Key]
        [Column ("id")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        
        [Display(Name = "Nombre OS")]
        [Required]
        public string nombre { get; set; }

        [Required]
        public string direccion { get; set; }

        [Required]
        public int telefono { get; set; }
        public int estado { get; set; }
        
    }
}