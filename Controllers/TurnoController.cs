using medTurno_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace medTurno_Api.Controllers
{
    public class TurnoController : Controller
    {
        private readonly ILogger<TurnoController> _logger;
        private readonly RepositorioTurno repositorioTurno;
        private readonly RepositorioPrestador repositorioPrestador;
        private readonly RepositorioUsuario repositorioUsuario;
        private readonly RepositorioDoctor repositorioDoctor;
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment environment;
        public TurnoController(ILogger<TurnoController> logger, IConfiguration config, IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.repositorioTurno = new RepositorioTurno(config);
            this.repositorioUsuario = new RepositorioUsuario(config);
            this.repositorioDoctor = new RepositorioDoctor(config);
            this.repositorioPrestador = new RepositorioPrestador(config);
            this.config = config;
            _logger = logger;
        }

        // GET: 
        [Authorize]
        [Route("Listar_Turnos", Name = "Listar_t")]
        public IActionResult Index()
        {
            var lta = repositorioTurno.obtenerTodos();
            ViewData[nameof(Turno)] = lta;
            return View();
        }

        // GET: 
        [Authorize]
        [Route("Listar_Hoy", Name = "Listar_h")]
        public IActionResult Hoy()
        {
            var lta = repositorioTurno.obtenerHoy();
            ViewData[nameof(Turno)] = lta;
            return View();
        }

        [Route("CalendarioTurnos", Name = "Calendario")]
        public void Calendario()
        {
            Response.Redirect("http://localhost/medTurno/medTurno_Api/Calendar?controller=turno&action=turno");
        }        

        [Authorize]
        public IActionResult Detalles(int id)
        { 
            Turno t = repositorioTurno.obtenerPorIdTurno(id);        
            return View(t);
        }

        [Authorize]
        public IActionResult CambiarEstados(string id)
        { 
            string colort = null;
            string[] subs = id.Split('-');
            int idt = Int32.Parse(subs[0]);
            int estadot = Int32.Parse(subs[1]);
            
            if(estadot==0){ colort = "#c31432";}
            if(estadot==1){ colort = "#e03197";}
            if(estadot==2){ colort = "#5b5be2";}            
            if(estadot==3){ colort = "#f5af19";} 
            if(estadot==4){ colort = "#30E8BF";}
            if(estadot==5){ colort = "#ff0000";}
            if(estadot==6){ colort = "#ffff0070";}
            if(estadot==7){ colort = "#e67e7e";}
            if(estadot==8){ colort = "#52af52";}

            repositorioTurno.Modificar(idt, estadot, colort);
            TempData["Mensaje"] = "Turno editado con Exito!!";        
            return RedirectToAction("Admin", "Home");
        }
    }
}
