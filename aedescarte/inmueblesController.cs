/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using medTurno_Api.Models;

namespace medTurno_Api.ApiController
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class inmueblesController : ControllerBase
    {
        private readonly medTurnoApiDbContext _context;

        public inmueblesController(medTurnoApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<InmuebleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario = User.Identity.Name;
                var res = await _context.Inmueble
                                    .Include(e => e.Duenio)
                                    .Where(e => e.Duenio.Email == usuario && e.Estado == 1)
                                    .ToListAsync();
                return Ok(res);                       
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<InmuebleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                var res = await _context.Inmueble.Include(e => e.Duenio)
                            .Where(e => e.Duenio.Email == usuario && e.Estado == 1)
                            .SingleAsync(e => e.Id_inmu == id);
                
                if(res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<InmuebleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Inmueble entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidad.id_propietario = _context.Propietarios.Single(e => e.Email == User.Identity.Name).Id;
                    _context.Inmueble.Add(entidad);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = entidad.Id_inmu }, entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<InmuebleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Inmueble entidad)
        {
            
            try
            {
                var user = User.Identity.Name;
                var res = _context.Inmueble.AsNoTracking()
                            .Include(e => e.Duenio)
                            .FirstOrDefault(e => e.Id_inmu == id && e.Duenio.Email == user);

                if (ModelState.IsValid && res != null)
                {
                    entidad.Id_inmu = id;
                    _context.Inmueble.Update(entidad);
                   await _context.SaveChangesAsync();

                   var cambios = _context.Inmueble.AsNoTracking()
                                    .Include(e => e.Duenio)
                                    .FirstOrDefault(e => e.Id_inmu == id && e.Duenio.Email == user);

                    return Ok(cambios);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<InmuebleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entidad = _context.Inmueble.Include(e => e.Duenio)
                            .FirstOrDefault(e => e.Id_inmu == id && e.Duenio.Email == User.Identity.Name);
                if (entidad != null)
                {
                    _context.Inmueble.Remove(entidad);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("BajaLogica/{id}")]
        public async Task<IActionResult> BajaLogica(int id)
        {
            try
            {
                var entidad = _context.Inmueble.Include(e => e.Duenio)
                            .FirstOrDefault(e => e.Id_inmu == id && e.Duenio.Email == User.Identity.Name);
                if (entidad != null)
                {
                    entidad.Estado = 0;//cambiar por estado = 0
                    _context.Inmueble.Update(entidad);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("disponibles")]
        public async Task<IActionResult> InmueblesDisponibles()
        {
            try
            {
                var usuario = User.Identity.Name;
                var res = await _context.Inmueble.Include(e => e.Duenio)
                                        .Where(e => e.Duenio.Email == usuario && e.Estado == 1)
                                        .ToListAsync();
                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("porFechas")]
        public async Task<IActionResult> BuscarInmueblesPorFecha([FromForm]BuscarPorFecha busqueda)
        {
            try
            {
                var usuario = User.Identity.Name;

                var res = await _context.Contrato.Include(e => e.Inmueble.Duenio)
                    .Where(e => e.Inmueble.Duenio.Email == usuario 
                            && (busqueda.FechaInicio <= e.fe_ini || busqueda.FechaInicio <=  e.fe_fin) 
                            && (busqueda.FechaFin >= e.fe_fin|| busqueda.FechaFin >= e.fe_fin)
                    )
                    .Select(x => new 
                    {
                        x.Inmueble.Direccion_in, x.Inmueble.ambientes, x.Inmueble.Uso, x.Inmueble.Tipo, x.Inquilino.Nombre 
                    })
                    .ToListAsync();
                    
                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private bool InmuebleExists(int id)
        {
            return _context.Inmueble.Any(e => e.Id_inmu == id);
        }
    }
}
 */