using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
using SQLitePCL;

namespace RegistroTecnicos.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options)
        : base(options) { }

    public DbSet <Tecnicos> Tecnicos { get; set; }
    public DbSet<TiposTecnicos> TiposTecnicos { get; set; }

    public DbSet<Clientes> Clientes { get; set; }

    public DbSet<Trabajos> Trabajos { get; set; }
    public DbSet<Prioridades> Prioridades { get; set; }
    public DbSet<Articulos> Articulos { get; set; }
    public DbSet<TrabajoDetalle> TrabajoDetalle { get; set; }
    public DbSet<Cotizaciones> Cotizaciones { get; set; }
    public DbSet<CotizacionesDetalle> CotizacionesDetalle { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Tecnicos>()
            .HasOne(tt => tt.TiposTecnicos)
            .WithMany(t => t.Tecnicos)
            .HasForeignKey(t => t.TiposTecnicosId);

        modelBuilder.Entity<Articulos>().HasData(new List<Articulos>()
        {
            new()
            {
                ArticuloId = 1,
                Descripcion = "Teclado",
                Costo = 1000.0m,
                Precio = 1500.0m,
                Existencia = 100
            },
            new()
            {
                ArticuloId = 2,
                Descripcion = "Tarjeta Grafica",
                Costo = 20000.0m,
                Precio = 30000.0m,
                Existencia = 50
            }
        }
    ); 

        base.OnModelCreating(modelBuilder);
    }


}
