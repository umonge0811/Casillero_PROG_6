using Casillero_PROG_6.Models;
using Microsoft.EntityFrameworkCore;

namespace Casillero_PROG_6.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed inicial de datos
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, nombre = "Electronico", porcentaje = 12 },
                new Categoria { Id = 2, nombre = "Hogar", porcentaje = 18 },
                new Categoria { Id = 3, nombre = "Industrial", porcentaje = 25 },
                new Categoria { Id = 4, nombre = "Restringido", porcentaje = 40 }
            );

            modelBuilder.Entity<Tarifa>().HasData(
                new Tarifa { Id = 1, nombre = "Súper Fast", Costo = 25 },
                new Tarifa { Id = 2, nombre = "Fast", Costo = 20 },
                new Tarifa { Id = 3, nombre = "Standar", Costo = 17 },
                new Tarifa { Id = 4, nombre = "Economic", Costo = 15 },
                new Tarifa { Id = 5, nombre = "Maritimo", Costo = 6 }
            );

            modelBuilder.Entity<Usuario>().HasData(
     new Usuario
     {
         Id = 1,
         NombreUsuario = "admin",
         Nombre = "Administrador",
         Email = "admin@casillero.com",
         Clave = "admin123",
         Tipo = 2,
         Telefono = "1234-5678" // Añadido el teléfono
     }
 );
        }
    }
}