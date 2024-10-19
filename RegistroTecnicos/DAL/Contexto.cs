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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Tecnicos>()
            .HasOne(tt => tt.TiposTecnicos)
            .WithMany(t => t.Tecnicos)
            .HasForeignKey(t => t.TiposTecnicosId);

        modelBuilder.Entity<Articulos>().HasData(
        new List<Articulos>()
        {
            new()
            {
                ArticuloId = 1,
                Descripcion = "Artículo A",

            },
            new()
            {
                ArticuloId = 2,
                Descripcion = "Artículo B",

            }
        }
    ); 

        base.OnModelCreating(modelBuilder);
    }

    


}
