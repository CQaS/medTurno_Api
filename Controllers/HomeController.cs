using medTurno_Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace medTurno_Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly RepositorioTurno repositorioTurno;
        private readonly RepositorioDoctor repositorioDoctor;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, IConfiguration config)
        {
            this.environment = environment;
            this.repositorioDoctor = new RepositorioDoctor(config);
            this.repositorioTurno = new RepositorioTurno(config);          
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Admin(int? id)
        {
            if(id == null)
            {
                string usuarioLogin = User.Identity.Name;

                string[] subs = usuarioLogin.Split('-');
                int _id = Int32.Parse(subs[0]);
                string av = subs[1];

                ViewBag.id = _id;
                ViewBag.av = av;

                ViewBag.Mensaje= TempData["Mensaje"];
                ViewBag.Error= TempData["Error"];
                var ltaP = repositorioDoctor.obtener();
                ViewData[nameof(Doctor)] = ltaP;
                var lta = repositorioTurno.ObtenerPorEstado();
                ViewData[nameof(Turno)] = lta;
                Turno t = new Turno();
                t.Id = -1;
                t.estado = -1;
                t.start = "";
                return View(t);
            }
            else
            {
                string usuarioLogin = User.Identity.Name;

                string[] subs = usuarioLogin.Split('-');
                int _id = Int32.Parse(subs[0]);
                string av = subs[1];

                ViewBag.id = _id;
                ViewBag.av = av;

                ViewBag.Mensaje= TempData["Mensaje"];
                ViewBag.Error= TempData["Error"];
                var lta = repositorioTurno.ObtenerPorEstado();
                ViewData[nameof(Turno)] = lta;
                var ltaP = repositorioDoctor.obtener();
                ViewData[nameof(Doctor)] = ltaP;
                Turno t = repositorioTurno.obtenerPorIdTurno(id);
                ViewBag.p = t.usuario.nombre;        
                return View(t);
            }
            
        }

        public IActionResult crear()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult Restringido()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
