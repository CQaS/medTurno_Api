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
    public class DireccionController : Controller
    {
        private readonly ILogger<DireccionController> _logger;

        private readonly RepositorioDireccion repositorioDireccion;
        private readonly RepositorioUsuario repositorioUsuario;
        private readonly IWebHostEnvironment environment;

        public DireccionController(ILogger<DireccionController> logger, IWebHostEnvironment environment, IConfiguration config)
        {
            this.environment = environment;
            this.repositorioDireccion = new RepositorioDireccion(config);
            this.repositorioUsuario = new RepositorioUsuario(config);
            _logger = logger;
        }

        // GET:
        [Authorize] 
        public IActionResult Index()
        {
            var lta = repositorioDireccion.obtener();
            ViewData[nameof(Direccion)] = lta;
            ViewData["listaInmu"] = "Nuestros Direcciones Actuales";
            return View();
        }

        // GET: 
        [Authorize]
        public IActionResult Alta()
        {
            var lta = repositorioUsuario.ObtenerTodos();
            ViewData[nameof(Usuario)] = lta;
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Alta(Direccion i)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    /* if(i.FotoFile !=null)
                    {
                        string wwwPath = environment.WebRootPath;
                        string path = Path.Combine(wwwPath, "img/fotos"+i.id_propietario);
                        if(!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileName = "Inmueble_" + DateTime.Now.ToString("dd_MM_yyyy") + DateTime.Now.ToString("hh_mm_ss") + Path.GetExtension(i.FotoFile.FileName);
                        string pathCompleto = Path.Combine(path, fileName);
                        i.foto = Path.Combine("/img/fotos"+i.id_propietario, fileName);
                        using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                        {
                            i.FotoFile.CopyTo(stream);
                        } */

                        //repositorioDireccion.Alta(i);                           
                             
                    /* }  */   
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Algo fallo en el Alta del Direccion, intenta nuevamente!";
                    var lta = repositorioUsuario.ObtenerTodos();
                    ViewData[nameof(Usuario)] = lta;
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
            var lta = repositorioUsuario.ObtenerTodos();
            ViewData[nameof(Usuario)] = lta;
            Direccion i = repositorioDireccion.Buscar(id);
            return View(i);
        }


        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Editar(Direccion i)
        {
            try
            {
                    /* var DireccionActual = repositorioDireccion.Buscar(i.Id_inmu);

                    if(i.FotoFile !=null)
                    {
                        string wwwPath = environment.WebRootPath;
                        string path = Path.Combine(wwwPath, "img/fotos"+i.id_propietario);
                        if(!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileName = "Inmueble_" + DateTime.Now.ToString("dd_MM_yyyy") + DateTime.Now.ToString("hh_mm_ss") + Path.GetExtension(i.FotoFile.FileName);
                        string pathCompleto = Path.Combine(path, fileName);
                        i.foto = Path.Combine("/img/fotos"+i.id_propietario, fileName);
                        using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                        {
                            i.FotoFile.CopyTo(stream);
                        }                         
                             
                    }
                    else
                    {
                        i.foto = inmuebleActual.foto;
                    } */
                    
                repositorioDireccion.Editar(i);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(i);
            }
        }

        [Authorize]
        public IActionResult Detalles(int id)
        {
            Direccion i = repositorioDireccion.Buscar(id); 
            return View(i);
        }

        /* [Authorize]
        public IActionResult VerContratos(int id)
        {
            var lta = repoContrato.VerContratosXDireccion(id);
            ViewData[nameof(Contrato)] = lta; 
            return View();
        } */

        // 
        [Authorize(Policy = "Administrador")]
        public IActionResult Delete(int id)
        {
            repositorioDireccion.Borrar(id);
            return RedirectToAction("Index");
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
