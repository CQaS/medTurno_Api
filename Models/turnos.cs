using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace medTurno_Api.Models
{
    public class Turno
    {
        [Column ("id")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Display(Name = "Fecha de Alta")]
        public string fechaSolicitud { get; set; }
        [Required]
        [Display(Name = "Fecha Solicitada")]
        public string start { get; set; } 
        public string end { get; set; }
        [Required]
        [Display(Name = "Descripcion")]  
        public string descripcion { get; set; }
        public string color { get; set; }
        [Display(Name = "Razon")]
        public string title { get; set; }
        [Required]
        public int idPrestador { get; set; }
        [Required]
        public int idDoctor { get; set; }
        [Required]
        public int idUsuario {get; set;} 
        [Display(Name = "Estado")]
        public int estado { get; set; }
        
        [ForeignKey(nameof(idUsuario))]
        public Usuario usuario { get; set;}

        [ForeignKey(nameof(idDoctor))]
        public Doctor doctor { get; set;}

        [ForeignKey(nameof(idPrestador))]
        public Prestador prestador { get; set;}       
    }
}