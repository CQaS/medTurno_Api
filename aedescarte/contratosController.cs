/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using medTurno_Api.Models;

namespace medTurno_Api.ApiController
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class contratosController : ControllerBase
    {
        private readonly medTurnoApiDbContext _context;

        public contratosController(medTurnoApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<ContratoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario = User.Identity.Name;
                var res = await _context.Contrato
                            .Include(x => x.Inquilino)
                            .Include(x => x.Inmueble)
                            .ThenInclude(x => x.Duenio)
                            .Where(c => c.Inmueble.Duenio.Email == usuario && (c.fe_fin >= DateTime.Now))
                            .ToListAsync();
    
                return Ok(res);


                
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }

        }

        // GET api/<ContratoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                var res = await _context.Contrato
                    .Include(x => x.Inquilino)
                    .Include(x => x.Inmueble)
                    .ThenInclude(x => x.Duenio)
                    .Where(c => c.Inmueble.Duenio.Email == usuario)
                    .SingleAsync(e => e.Id == id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }




        // POST api/<ContratoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Contrato contrato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = User.Identity.Name;
                    contrato.id_inmueble = _context.Inmueble
                                            .FirstOrDefault(e => e.Duenio.Email == usuario).Id_inmu;
                    contrato.id_inmueble = _context.Inquilinos
                                            .Single(e => e.Id == contrato.id_inquilino).Id;

                    _context.Contrato.Add(contrato);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(Get), new { id =contrato.Id }, contrato);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ContratoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Contrato contrato)
        {
            try
            {
                var usuario = User.Identity.Name;
                var res = _context.Contrato
                                .AsNoTracking()
                                .Include(e => e.Inmueble)
                                .ThenInclude(x => x.Duenio)
                                .FirstOrDefault(e => e.Id == id && e.Inmueble.Duenio.Email == usuario);

                if (ModelState.IsValid && res != null)
                {
                   
                    contrato.Id = id;
                    _context.Contrato.Update(contrato);
                    await _context.SaveChangesAsync();

                    var cambios = _context.Contrato
                                .AsNoTracking()
                                .Include(e => e.Inmueble)
                                .ThenInclude(x => x.Duenio)
                                .FirstOrDefault(e => e.Id == id && e.Inmueble.Duenio.Email == usuario);


                    return Ok(cambios);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    

        // DELETE api/<ContratoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = User.Identity.Name;
                var entidad = _context.Contrato
                                        .Include(e => e.Inmueble.Duenio)
                                        .FirstOrDefault(e => e.Id == id && e.Inmueble.Duenio.Email == usuario); ;
                
                if (entidad != null)
                {
                    _context.Contrato.Remove(entidad);
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
        [HttpGet("GetPropietariosVigentes")]
        public async Task<IActionResult> GetPropietariosVigentes() 
        {

            try
            {
                var user = User.Identity.Name;
                var res = await _context.Contrato
                            .Include(x => x.Inquilino)
                            .Include(x => x.Inmueble)
                            .ThenInclude(x => x.Duenio)
                            .Where(y => y.Inmueble.Duenio.Email == user && (y.fe_fin >= DateTime.Now))
                            .ToListAsync();
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
      }
      
    }
}


file:///D:/Documentos/TUDS%20202X/WEB/CSS/404PageNotFound.html
file:///D:/Documentos/TUDS%20202X/WEB/CSS/CheckboxList.html
file:///D:/Documentos/TUDS%20202X/WEB/CSS/ListDesignCoolHover.html ...
file:///D:/Documentos/TUDS%20202X/WEB/CSS/ListItemHoverEffects.html ...
file:///D:/Documentos/TUDS%20202X/WEB/CSS/PostListCalendar.html ...
file:///D:/Documentos/TUDS%20202X/WEB/CSS/SocialMediaIconHover.html
 */