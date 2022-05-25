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
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        private readonly RepositorioUsuario repositorioUsuario;
        private readonly RepositorioPrestador repositorioPrestador;
        private readonly RepositorioDireccion repositorioDireccion;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public UsuarioController(ILogger<UsuarioController> logger, IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.configuration = configuration;
            this.repositorioUsuario = new RepositorioUsuario(configuration);
            this.repositorioPrestador = new RepositorioPrestador(configuration);
            this.repositorioDireccion = new RepositorioDireccion(configuration);
            _logger = logger;
        }

        // GET: Usuario
        [Authorize(Policy = "Administrador")]
        [Route("Listar_Pacientes", Name = "Listar_p")]
        public ActionResult Index()
        {
            var usuarios = repositorioUsuario.ObtenerTodos();
            ViewData[nameof(Usuario)] = usuarios;
            
            ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            return View();
        }

        // GET: Usuario/Create
        [Authorize(Policy = "Administrador")]
        [Route("Alta_Pacientes", Name = "Alta_p")]
        public ActionResult Alta()
        {
            ViewBag.Roles = Usuario.ObtenerRoles();
            var prestador = repositorioPrestador.obtener();
            ViewData[nameof(Prestador)] = prestador;
            var direccion = repositorioDireccion.obtener();
            ViewData[nameof(Direccion)] = direccion;
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        [Route("Alta_Pacientes", Name = "Alta_p")]
        public ActionResult Alta(Usuario u)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Roles = Usuario.ObtenerRoles();
                    var prestador = repositorioPrestador.obtener();
                    ViewData[nameof(Prestador)] = prestador;
                    var direccion = repositorioDireccion.obtener();
                    ViewData[nameof(Direccion)] = direccion;
                    if(u.Id == 0)
                       TempData["Mensaje"] = "Debe ingresar todo los datos del usuario!";
                       ViewBag.Error= TempData["Mensaje"];
                    return View();
                }
                {
                    var user = repositorioUsuario.ObtenerPorEmail(u.Mail);

                    if (user == null )
                    {
                        int idDeLaDireccion;
                        if(u.idDireccion > 0)
                        {
                            idDeLaDireccion = u.idDireccion;
                        }
                        else
                        {
                            if(String.IsNullOrEmpty(u.direccion.calle) || u.direccion.numero == 0 || String.IsNullOrEmpty(u.direccion.ciudad))
                            {                            
                                ViewBag.Roles = Usuario.ObtenerRoles();
                                var prestador = repositorioPrestador.obtener();
                                ViewData[nameof(Prestador)] = prestador;
                                var direccion = repositorioDireccion.obtener();
                                ViewData[nameof(Direccion)] = direccion;
                                if(u.Id == 0)
                                TempData["Mensaje"] = "Debe ingresar todo los datos del usuario!";
                                ViewBag.Error = TempData["Mensaje"];
                                return View();    
                            }
                            else
                            {
                                idDeLaDireccion = repositorioDireccion.Alta(u.direccion.calle, u.direccion.numero, u.direccion.ciudad);
                            }                          
                        }
                        
                        if(u.password !=null)
                        {
                            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: u.password,
                                salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                                prf: KeyDerivationPrf.HMACSHA1,
                                iterationCount: 1000,
                                numBytesRequested: 256 / 8));
                            u.password = hashed;
                        }
                        else
                        {
                                ViewBag.Roles = Usuario.ObtenerRoles();
                                var prestador = repositorioPrestador.obtener();
                                ViewData[nameof(Prestador)] = prestador;
                                var direccion = repositorioDireccion.obtener();
                                ViewData[nameof(Direccion)] = direccion;
                                if(u.Id == 0)
                                TempData["Mensaje"] = "Debe ingresar todo los datos del usuario!";
                                ViewBag.Error= TempData["Mensaje"];
                                return View();
                        }
                        

                        if(u.Rol == 1){ u.Avatar = "/img/avatars/admin.jpg"; }
                        if(u.Rol == 2){ u.Avatar = "/img/avatars/Empleado.png"; }
                        if(u.Rol == 3){ u.Avatar = "/img/avatars/person.png"; }
                        
                        var alta = repositorioUsuario.Alta(u, idDeLaDireccion);

                        TempData["Id"] = u.Id;
                        TempData["Mensaje"] = "Datos ingresados correctamente!";
                        ViewBag.Error= TempData["Mensaje"];
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Mensaje"] = "El Email o Usuario ingresado ya se encuentra registrado en el sistema!";
                        ViewBag.Error = TempData["Mensaje"];
                        ViewBag.Roles = Usuario.ObtenerRoles();
                        var prestador = repositorioPrestador.obtener();
                        ViewData[nameof(Prestador)] = prestador;
                        var direccion = repositorioDireccion.obtener();
                        ViewData[nameof(Direccion)] = direccion;
                        return View();
                    }                  

                }                
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                ViewBag.Roles = Usuario.ObtenerRoles();
                var prestador = repositorioPrestador.obtener();
                ViewData[nameof(Prestador)] = prestador;
                var direccion = repositorioDireccion.obtener();
                ViewData[nameof(Direccion)] = direccion;
               
                return View();
            }
        }


        // GET: Usuario/Editar/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Editar(int id)
        {
            ViewData["Title"] = "Editar usuario";
            var u = repositorioUsuario.ObtenerPorId(id);
            
            ViewBag.Roles = Usuario.ObtenerRoles();
            var prestador = repositorioPrestador.obtener();
            ViewData[nameof(Prestador)] = prestador;
            var direccion = repositorioDireccion.obtener();
            ViewData[nameof(Direccion)] = direccion;

            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(u);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Editar(int id, Usuario u)
        {
            //var vista = "Editar";
            try
            {
                if(!ModelState.IsValid)
                {
                    var users = repositorioUsuario.ObtenerPorId(id);
            
                    ViewBag.Roles = Usuario.ObtenerRoles();
                    var prestador = repositorioPrestador.obtener();
                    ViewData[nameof(Prestador)] = prestador;
                    var direccion = repositorioDireccion.obtener();
                    ViewData[nameof(Direccion)] = direccion;
                    TempData["Mensaje"] = "Debe ingresar todo los datos del usuario!";
                    ViewBag.Error= TempData["Mensaje"];
                    return View(users);
                }
                else
                {
                        var usuarioAeditar = repositorioUsuario.ObtenerPorId(id);

                        if(u.Mail != usuarioAeditar.Mail)
                        {
                            var mailNuevo = repositorioUsuario.ObtenerPorEmail(u.Mail);

                            if(mailNuevo != null)
                            {
                                var users = repositorioUsuario.ObtenerPorId(id);
            
                                ViewBag.Roles = Usuario.ObtenerRoles();
                                var prestador = repositorioPrestador.obtener();
                                ViewData[nameof(Prestador)] = prestador;
                                var direccion = repositorioDireccion.obtener();
                                ViewData[nameof(Direccion)] = direccion;
                                TempData["Mensaje"] = "El E-mail ya existe!";
                                ViewBag.Error= TempData["Mensaje"];
                                return View(users);
                            
                            }
                        }

                        if(u.password != null)
                        {
                            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: u.password,
                                salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                                prf: KeyDerivationPrf.HMACSHA1,
                                iterationCount: 1000,
                                numBytesRequested: 256 / 8));
                            u.password = hashed;
                        }
                        else
                        {
                            u.password = usuarioAeditar.password;
                        }

                        if(u.direccion.calle != null && u.direccion.numero > 0 && u.direccion.ciudad !=null)
                        {
                          u.idDireccion = repositorioDireccion.Alta(u.direccion.calle, u.direccion.numero, u.direccion.ciudad);
                        }

                        repositorioUsuario.Modifica(u);
                        TempData["Mensaje"] = "Datos guardados correctamente";
                        if (TempData.ContainsKey("Mensaje"))
                            ViewBag.Error = TempData["Mensaje"];

                        return RedirectToAction(nameof(Index));
                    
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                var users = repositorioUsuario.ObtenerPorId(id);
            
                ViewBag.Roles = Usuario.ObtenerRoles();
                var prestador = repositorioPrestador.obtener();
                ViewData[nameof(Prestador)] = prestador;
                var direccion = repositorioDireccion.obtener();
                ViewData[nameof(Direccion)] = direccion;
                TempData["Error"] = "Algo fallo, intenta nuevamente!";
                ViewBag.Error= TempData["Error"];
                return View(users);
            }
        }

        // GET: Usuario/Delete/5
        [HttpDelete]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            try
            {
                repositorioUsuario.Borrar(id);
                return Json(new { success = true, menssage = "Usuario dado de baja con Exito!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, menssage = "Algo fallo, contacta a Admin!" + ex });
            }
        }

	    [AllowAnonymous]
        // GET: Usuario/Login/
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView login)
        {
            try
            {
                //var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as String) ? "/Home" : TempData["returnUrl"].ToString();
                if (ModelState.IsValid)
                {
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: login.Password,
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));

                    var e = repositorioUsuario.ObtenerPorEmail(login.Usuario);

                    if(String.IsNullOrEmpty(login.Pregunta))
                    {
                        if (e == null || e.password != hashed)
                        {
                            TempData["Mensaje"] = "El E-mail o Password no son correctos";
                            ViewBag.Error= TempData["Mensaje"];
                            return View();
                        }

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, e.Id + "-" + e.Avatar),
                            new Claim("FullName", e.nombre),
                            new Claim(ClaimTypes.Role, e.RolNombre),
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));

                        repositorioUsuario.session(login.Usuario);

                        return RedirectToAction(nameof(Index), "Home");
                    }
                    else
                    {
                        if (e == null || login.Pregunta != e.pregunta)
                        {
                            ModelState.AddModelError("", "Los datos no son correctos");
                            return RedirectToAction(nameof(Index), "Home");
                        }
                        else
                        { 
                            repositorioUsuario.ModificarPass(e.Mail, hashed); 
                            TempData["Mensaje"] = "Todo OK. Password Renovado!";
                            ViewBag.Mensaje = TempData["Mensaje"];
                            return RedirectToAction(nameof(Index), "Home");
                        }
                    }
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: Usuario/recuperaPass
        [Route("Recuperar_password", Name = "Recuperar_password")]
        public ActionResult RecuperaPass()
        {
            return View();
        }

        // GET: Usuario/Logout 6377048774
        [Route("Salida", Name = "Logout")]
        public async Task<ActionResult> Logout()
        {
            var hashLog = "";
            repositorioUsuario.session(hashLog);

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
