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
    public class doctoresController : ControllerBase
    {
        private readonly medTurnoApiDbContext _context;

        public doctoresController(medTurnoApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<DoctorController> lista de Doctores
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _context.Doctor.OrderBy(a => a.nombre)
                                                .Include(x => x.especialidad)
                                                .Where(x => x.idEspecialidad == x.especialidad.id)
                                                .Select(a => new 
                                                { 
                                                    a.id, a.nombre, a.matricula, a.especialidad.especialidad
                                                })
                                                .ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return NotFound();

            var res = await _context.Doctor.FirstOrDefaultAsync(x => x.id == id);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }


        // POST api/<DoctorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Doctor doctor)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Doctor.Add(doctor);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(Get), new { id = doctor.id }, doctor);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Doctor.Update(doctor);
                    await _context.SaveChangesAsync();

                    return Ok(doctor);
                }

                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        private bool DocExists(int id)
        {
            return _context.Doctor.Any(e => e.id == id);
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var p = _context.Doctor.Find(id);
                    if (p == null)
                        return NotFound();
                    _context.Doctor.Remove(p);
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
    }
}
