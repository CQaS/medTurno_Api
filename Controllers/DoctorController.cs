using medTurno_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Claims;

namespace medTurno_Api.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<DoctorController> _logger;

        private readonly RepositorioDoctor repositorioDoctor;
        private readonly RepositorioEspecialidad repositorioEspecialidad;
        private readonly IWebHostEnvironment environment;

        public DoctorController(ILogger<DoctorController> logger, IWebHostEnvironment environment, IConfiguration config)
        {
            this.environment = environment;
            this.repositorioDoctor = new RepositorioDoctor(config);
            this.repositorioEspecialidad = new RepositorioEspecialidad(config);
            _logger = logger;
        }

        // GET:
        [Authorize]
        [Route("Listar_Doctores", Name = "Listar_d")] 
        public IActionResult Index()
        {
            var lta = repositorioDoctor.obtener();
            ViewData[nameof(Doctor)] = lta;
            ViewData["listaDoc"] = "Nuestros Doctores Actuales";
            return View();
        }

        // GET: 
        [Authorize]
        [Route("Alta_Doctores", Name = "Alta_d")]
        public IActionResult Alta()
        {
            var lta = repositorioEspecialidad.obtener();
            ViewData[nameof(Especialidad)] = lta;
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Route("Alta_Doctores", Name = "Alta_d")]
        public IActionResult Alta(Doctor i)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    repositorioDoctor.Alta(i);
                    TempData["Mensaje"] = "Profecional dado de Alta con Exito!!";
                    return RedirectToAction("Admin", "Home");
                }
                else
                {
                    ViewBag.Error = "Verifica los datos del Doctor, intenta nuevamente!";
                    var lta = repositorioEspecialidad.obtener();
                    ViewData[nameof(Especialidad)] = lta;
                    return View();
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
            var lta = repositorioEspecialidad.obtener();
            ViewData[nameof(Especialidad)] = lta;
            Doctor i = repositorioDoctor.Buscar(id);
            return View(i);
        }


        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Editar(Doctor i)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    repositorioDoctor.Editar(i);
                    TempData["Mensaje"] = "Profecional editado con Exito!!";
                    return RedirectToAction("Admin", "Home");
                }
                else
                {
                    ViewBag.Error = "Verifica los datos del Doctor, intenta nuevamente!";
                    var lta = repositorioEspecialidad.obtener();
                    ViewData[nameof(Especialidad)] = lta;
                    return View(i);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                var lta = repositorioEspecialidad.obtener();
                ViewData[nameof(Especialidad)] = lta;
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
                repositorioDoctor.Borrar(id);
                return Json(new { success = true, menssage = "Profecional dado de baja con Exito!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, menssage = "Algo fallo, contacta a Admin! " + ex });
            }
            
        }

    }
}
