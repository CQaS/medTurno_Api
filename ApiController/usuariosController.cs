using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using medTurno_Api.Models;

namespace medTurno_Api.ApiController
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly medTurnoApiDbContext _context;
        private readonly IConfiguration config;

        public usuariosController(medTurnoApiDbContext context, IConfiguration config)
        {
            _context = context;
            this.config = config;
        }

        // GET: api/usuarios devuelve el Paciente logueado
        [HttpGet]
        public async Task<ActionResult<Usuario>> Get()
        {
            try
            {
                var usuario = User.Identity.Name;

                return await _context.Usuario
                            .Include(x => x.direccion)
                            .SingleOrDefaultAsync(x => x.Mail == usuario && x.estado != 0);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    

        // GET api/<usuarioController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var usuario = User.Identity.Name;

                if (id <= 0)
                    return NotFound();

                var res = await _context.Usuario.SingleOrDefaultAsync(x => x.Id == id && x.Mail == usuario);
                if (res != null)
                    return Ok(res);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<usuarioController>/5
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] Usuario usuario)
        {
            
            try
            {
               
                var res = await _context.Usuario.FirstOrDefaultAsync(x => x.Mail == usuario.Mail);

                if ((ModelState.IsValid )&&(res != null))
                {
                    _context.Usuario.Update(usuario);
                    await _context.SaveChangesAsync();
                    
                    return Ok(usuario);
                }

                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


        }
        // PUT = UPDATE api/<usuarioController>/ editar usuario
        [HttpPut]
        public async Task<IActionResult> Put(Usuario u)
        {
            try
            {               
                var usuario = User.Identity.Name;
                var res = _context.Usuario.AsNoTracking()
                                .FirstOrDefault(x => x.Mail == usuario && x.Mail == u.Mail);
                
                if ((ModelState.IsValid) && (res != null))
                {
                    if(u.password != res.password)
                    {
                        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: u.password,
                                salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                                prf: KeyDerivationPrf.HMACSHA1,
                                iterationCount: 1000,
                                numBytesRequested: 256 / 8));
                        u.password = hashed;
                    }

                    _context.Usuario.Update(u);
                    await _context.SaveChangesAsync();
                    
                    var cambios = _context.Usuario.AsNoTracking()
                                .FirstOrDefault(x => x.Mail == usuario  && x.Mail == u.Mail);
                
                    return Ok(cambios);
                }

                return BadRequest();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!UsuarioExists(u.Id))
                {
                    return NotFound();
                }
                else 
                {
                    throw;
                }
            }            
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e=>e.Id == id);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    var p = await _context.Usuario.FindAsync(id);
                    if (p == null)
                    {
                        return NotFound();
                    }

                    _context.Usuario.Remove(p);
                   await _context.SaveChangesAsync();
                   
                   return Ok(p);
                }

                return BadRequest();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }


        // POST api/<controller>/5
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]LoginView loginView)
        {
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: loginView.Password,
                    salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                
                var p = await _context.Usuario.FirstOrDefaultAsync(x => x.Mail == loginView.Usuario && x.estado != 0);

                if (p == null || p.password != hashed)
                {
                    return BadRequest("Nombre de usuario o clave incorrecta");
                }
                else
                {                    
                    var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
                    var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, p.Mail),
                        new Claim("FullName", p.nombre),
                        new Claim(ClaimTypes.Role, "Usuario"),
                    };

                    var token = new JwtSecurityToken
                    (
                        issuer: config["TokenAuthentication:Issuer"],
                        audience: config["TokenAuthentication:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(300),
                        signingCredentials: credenciales
                    );
                    
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
