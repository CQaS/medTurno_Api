using System;
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
    public class turnosController : ControllerBase
    {
        private readonly medTurnoApiDbContext _context;

        public turnosController(medTurnoApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<TurnoController> Lista de Turnos del Paciente Logueado
        [HttpGet]
        public async Task<ActionResult<Turno>> Get()
        {
            try
            {
                var usuario = User.Identity.Name;
                var usLog = await _context.Usuario.SingleOrDefaultAsync(x => x.Mail == usuario);
                
                var res = await _context.Turnos
                                        .Include(e => e.doctor)
                                        .Where(e => e.idUsuario == usLog.Id && e.estado != 0)
                                        .ToListAsync();
                                       

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<TurnoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {         
            try
            {
                var usuario = User.Identity.Name;
                var res = await _context.Turnos
                                        .Include(e => e.usuario)
                                        .Where(e => e.usuario.Mail == usuario && e.idUsuario == id)
                                        .Select(x => new 
                                        { 
                                           x.Id, x.estado, x.start, x.doctor.nombre
                                        })
                                        .ToListAsync();

                if(res != null)
                {
                   return Ok(res); 
                }

                return BadRequest();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<TurnoController>
        [HttpPost]
        public async Task<IActionResult> Post(Turno turno)
        {
            try
            {
                var usuario = User.Identity.Name;
                var usLog = await _context.Usuario.SingleOrDefaultAsync(x => x.Mail == usuario);

                turno.title = "Solicitado por Paciente en APP";
                turno.end = turno.start;
                turno.idPrestador = usLog.idPrestador;
                turno.idUsuario = usLog.Id;
                turno.color = "#5b5be2"; //pendiente
                turno.estado = 2; //pendiente
                turno.fechaSolicitud = DateTime.Now.ToString("yyyy-MM-dd");

                if (ModelState.IsValid)
                {
                    _context.Turnos.Add(turno);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = turno.Id }, turno);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT = UPDATE api/<TurnoController>/5
        [HttpPut]
        public async Task<IActionResult> Put(int id)
        {
            try
            {
                var turno = await _context.Turnos.SingleOrDefaultAsync(x => x.Id == id);
                turno.estado = 0;
                                        
                if (ModelState.IsValid)
                {
                    _context.Turnos.Update(turno);
                    await _context.SaveChangesAsync();

                    return Ok("OK");
                }
                
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
