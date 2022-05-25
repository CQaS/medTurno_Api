using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace medTurno_Api.Models
{
    public class Doctor
    {
        [Key]
        [Column ("id")]
        [Display(Name = "Codigo")]        
        public int id { get; set; }
        [Required]
        [Display(Name = "Profecional")] 
        public string nombre { get; set; }
        [Required]
        public string matricula { get; set; }
        [Required]
        public string horarioatencion { get; set; }
        public int estado { get; set; }
        public int idEspecialidad { get; set; }

        [ForeignKey(nameof(idEspecialidad))]
        public Especialidad especialidad { get; set; }
    }
}

