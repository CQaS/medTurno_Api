using medTurno_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace medTurno_Api.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly ILogger<EspecialidadController> _logger;

        private readonly RepositorioEspecialidad repositorioEspecialidad;
        private readonly RepositorioDoctor repositorioDoctor;

        public EspecialidadController(ILogger<EspecialidadController> logger, IConfiguration config)
        {
            this.repositorioEspecialidad = new RepositorioEspecialidad(config);
            this.repositorioDoctor = new RepositorioDoctor(config);
            _logger = logger;
        }

        // GET:
        [Authorize]
        [Route("Listar_Especialidades", Name = "Listar_e")]
        public IActionResult Index()
        {
            var lta = repositorioEspecialidad.obtener();
            ViewData[nameof(Especialidad)] = lta;
            return View();
        }

        // GET:
        [Authorize] 
        [Route("Alta_Especialidades", Name = "Alta_e")]
        public IActionResult Alta()
        {            
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Route("Alta_Especialidades", Name = "Alta_e")]
        public IActionResult Alta(Especialidad i)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    repositorioEspecialidad.Alta(i);
                    TempData["Mensaje"] = "Especialidad de Alta con Exito!!";
                    return RedirectToAction("Admin", "Home");
                }
                else
                {
                    ViewBag.Error = "Verifica los datos, intenta nuevamente!";
                    return View(i);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;
                return RedirectToAction("Index");
            }
        }

        // GET
        [Authorize]
        public IActionResult Editar(int id)
        {
            Especialidad i = repositorioEspecialidad.Buscar(id);
            return View(i);
        }


        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Editar(Especialidad i)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    repositorioEspecialidad.Editar(i);
                    TempData["Mensaje"] = "Especialidad editada con Exito!!";
                    return RedirectToAction("Admin", "Home");
                }
                else
                {
                    ViewBag.Error = "Verifica los datos, intenta nuevamente!";
                    return View(i);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(i);
            }
        }

        // 
        [HttpDelete]
        [Authorize(Policy = "Administrador")]
        public IActionResult Delete(int id)
        {
            try
            {
                repositorioEspecialidad.Borrar(id);
                return Json(new { success = true, menssage = "Especialidad dada de baja con Exito!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, menssage = "Algo fallo, contacta a Admin!" + ex });
            }
        }
    }
}
