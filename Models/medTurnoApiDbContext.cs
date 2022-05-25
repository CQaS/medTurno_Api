using Microsoft.EntityFrameworkCore;

namespace medTurno_Api.Models
{
    public class medTurnoApiDbContext : DbContext
    {
        public medTurnoApiDbContext(DbContextOptions<medTurnoApiDbContext> options)
        :base (options){}
        
        
        public DbSet<Usuario> Usuario{get; set;}
        //........<nomb en clase>..nomb en base
        public DbSet<Turno> Turnos{get; set;}
        public DbSet<Prestador> Prestador{get; set;}
        public DbSet<Historiaclinica> Historiaclinica{get; set;}
        public DbSet<Especialidad> Especialidad{get; set;}
        public DbSet<Doctor> Doctor{get; set;}
        public DbSet<Direccion> Direccion{get; set;}
    }
}