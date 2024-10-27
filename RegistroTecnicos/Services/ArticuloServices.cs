using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ArticuloServices(IDbContextFactory<Contexto> DbFactory)
{
  
    public async Task<List<Articulos>> ListaArticulos()
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Articulos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Articulos?> ObtenerArticuloPorId(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Articulos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ArticuloId == id);
    }

    public async Task ActualizarExistencia(int articuloId, decimal cantidad)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        var articulo = await _contexto.Articulos.FindAsync(articuloId);
        if (articulo != null)
        {
            articulo.Existencia -= cantidad; 
            _contexto.Articulos.Update(articulo);
            await _contexto.SaveChangesAsync();
        }
    }

    public async Task AgregarCantidad(int articuloId, int cantidad)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        var articulo = await _contexto.Articulos.FindAsync(articuloId);

        if (articulo != null)
        {
            articulo.Existencia += cantidad;

            await _contexto.SaveChangesAsync();
        }
    }

}