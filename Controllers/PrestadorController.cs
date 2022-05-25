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
    public class PrestadorController : Controller
    {
        private readonly ILogger<PrestadorController> _logger;

        private readonly RepositorioPrestador repositorioPrestador;

        public PrestadorController(ILogger<PrestadorController> logger, IConfiguration config)
        {
            this.repositorioPrestador = new RepositorioPrestador(config);
            _logger = logger;
        }

        // GET: 
        [Authorize]
        [Route("Listar_OSocial", Name = "Listar_OS")]
        public IActionResult Index()
        {
            var lta = repositorioPrestador.obtener();

            if(lta != null)
            {
               ViewData[nameof(Prestador)] = lta; 
            }
            else
            {
                TempData["Error"] = "Prestador dado de Alta con Exito!!";
                return RedirectToAction("Admin", "Home");
            }
            
            return View();
        }

        // GET: 
        [Authorize]
        [Route("Alta_OSocial", Name = "Alta_OS")]
        public IActionResult Alta()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Route("Alta_OSocial", Name = "Alta_OS")]
        public IActionResult Alta(Prestador i)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    repositorioPrestador.Alta(i);
                    TempData["Mensaje"] = "Prestador dado de Alta con Exito!!";
                    return RedirectToAction("Admin", "Home");
                }
                else
                {
                    ViewBag.Error = "Verifica que los datos esten correctos";
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
            Prestador i = repositorioPrestador.Buscar(id);
            return View(i);
        }


        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Editar(Prestador i)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    repositorioPrestador.Editar(i);
                    TempData["Mensaje"] = "Prestador editado con Exito!!";
                    ViewBag.Mensaje = TempData["Mensaje"];
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Verifica que los datos esten correctos";
                    ViewBag.Error = TempData["Error"];
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
                repositorioPrestador.Borrar(id);
                return Json(new { success = true, menssage = "Prestador dado de baja con Exito!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, menssage = "Algo fallo, contacta a Admin!" + ex });
            }
        }
    }
}
