using domain_layer.Admin;
using domain_layer.Personas;
using domain_layer.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access
{
    public class DatabaseContext : IdentityDbContext<SystemUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TipoSangre>(e => e.ToTable("TipoSangre"));
            modelBuilder.Entity<Persona>(e => e.ToTable("Persona"));
            modelBuilder.Entity<EstadoCivil>(e => e.ToTable("EstadoCivil"));
            modelBuilder.Entity<Genero>(e => e.ToTable("Genero"));
            modelBuilder.Entity<Empleado>(e => e.ToTable("Empleado"));

        }

        public DbSet<TipoSangre> TiposSangre { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<EstadoCivil> EstadosCiviles { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
    }
}
