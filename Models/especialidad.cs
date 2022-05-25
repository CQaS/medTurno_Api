using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace medTurno_Api.Models
{
    public class Especialidad
    {
        [Key]
        [Column ("id")]
        [Display(Name = "Codigo")]        
        public int id { get; set; }
        [Required]
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
        [Required]
        [Display(Name = "Especialidad Medica")]
        public string especialidad { get; set; }
        
    }
}