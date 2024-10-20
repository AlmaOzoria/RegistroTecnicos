using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TrabajosServices
{
    private readonly Contexto _contexto;

    public TrabajosServices(Contexto contexto)
    {

        _contexto = contexto;

    }

    public async Task<bool> Insertar(Trabajos trabajos)
    {
        _contexto.Trabajos.Add(trabajos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int trabajosId)
    {
        return await _contexto.Trabajos.AnyAsync(t => t.TrabajosId == trabajosId);

    }

    public async Task<bool> Modificar(Trabajos trabajos)
    {
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
        var trabajos = await _contexto.Trabajos
            .Where(t => t.TrabajosId == id)
            .ExecuteDeleteAsync();
        return trabajos > 0;

    }

    public List<Trabajos> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return _contexto.Trabajos
            .AsNoTracking()
            .Include(t => t.clientes)
            .Include(t => t.tecnicos)
            .Include(t => t.prioridades)
            .Include(t => t.TrabajoDetalle)
            .Where(criterio)
            .OrderBy(t=> t.prioridades.PrioridadesId)
            .ToList();
    }

    public async Task<Trabajos?> Buscar(int id)
    {
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
        var detalles = await _contexto.TrabajoDetalle
            .Where(t => t.Trabajos.TrabajosId == trabajosID)
            .ToListAsync();

        return detalles;
    }

    public async Task<List<Clientes>> ObtenerClientes()
    {
        return await _contexto.Clientes.ToListAsync();
    }

    public async Task<List<Tecnicos>> ObtenerTecnicos()
    {
        return await _contexto.Tecnicos.ToListAsync();
    } 
    public async Task<List<Prioridades>> ObtenerPrioridades()
    {
        return await _contexto.Prioridades.ToListAsync();
    } 
    
    public async Task<List<TrabajoDetalle>> ObtenerDetalle()
    {
        return await _contexto.TrabajoDetalle.ToListAsync();
      
    }    
    public async Task<List<Articulos>> ObtenerArticulos()
    {
        return await _contexto.Articulos.ToListAsync();
    }

    


}
