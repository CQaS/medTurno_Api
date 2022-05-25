using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace medTurno_Api.Models
{
    public class Direccion
    {
        [Key]
        [Column ("id")]
        [Display(Name = "Codigo")]        
        public int id { get; set; }
        public string calle { get; set; }
        public int numero { get; set; }
        public string ciudad { get; set; }

        public int estado { get; set; }
    }
}

