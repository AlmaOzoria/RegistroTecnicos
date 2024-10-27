using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TrabajosServices(IDbContextFactory<Contexto> DbFactory)
{
 

    public async Task<bool> Insertar(Trabajos trabajos)
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Trabajos.Add(trabajos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int trabajosId)
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Trabajos.AnyAsync(t => t.TrabajosId == trabajosId);

    }

    public async Task<bool> Modificar(Trabajos trabajos)
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Update(trabajos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Trabajos trabajos)
    {
        
        if (!await Existe(trabajos.TrabajosId))
            return await Insertar(trabajos);
        else
        {
            return await Modificar(trabajos);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        var trabajos = await _contexto.Trabajos
            .Where(t => t.TrabajosId == id)
            .ExecuteDeleteAsync();
        return trabajos > 0;

    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Trabajos
            .AsNoTracking()
            .Include(t => t.clientes)
            .Include(t => t.tecnicos)
            .Include(t => t.prioridades)
            .Include(t => t.TrabajoDetalle)
            .Where(criterio)
            .OrderBy(t => t.prioridades.PrioridadesId)
            .ToListAsync();
    }


    public async Task<Trabajos?> Buscar(int id)
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Trabajos
            .AsNoTracking()
            .Include(t => t.clientes)
            .Include(t => t.tecnicos)
            .Include(t => t.prioridades)
            .Include(t => t.TrabajoDetalle)
            .ThenInclude(td => td.Articulos)
            .FirstOrDefaultAsync(t => t.TrabajosId == id);
    }

    public async Task<List<TrabajoDetalle>> ObtenerDetallesTrabajoId(int trabajosID)
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        var detalles = await _contexto.TrabajoDetalle
            .Where(t => t.Trabajos.TrabajosId == trabajosID)
            .ToListAsync();

        return detalles;
    }

    public async Task<List<Clientes>> ObtenerClientes()
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Clientes.ToListAsync();
    }

    public async Task<List<Tecnicos>> ObtenerTecnicos()
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Tecnicos.ToListAsync();
    } 
    public async Task<List<Prioridades>> ObtenerPrioridades()
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Prioridades.ToListAsync();
    } 
    
    public async Task<List<TrabajoDetalle>> ObtenerDetalle()
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.TrabajoDetalle.ToListAsync();
      
    }    
    public async Task<List<Articulos>> ObtenerArticulos()
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Articulos.ToListAsync();
    }

    


}
