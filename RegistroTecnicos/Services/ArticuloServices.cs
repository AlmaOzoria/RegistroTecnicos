using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ArticuloServices
{
    private readonly Contexto _contexto;
    public ArticuloServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int servicioId)
    {
        return await _contexto.Servicio.AnyAsync(s => s.ServicioId == servicioId);
    }

    private async Task<bool> Insertar(Articulos servicio)
    {
        _contexto.Servicio.Add(servicio);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Articulos servicio)
    {
        _contexto.Servicio.Update(servicio);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(servicio).State = EntityState.Detached;
        return modificado;
    }

    public async Task<bool> Guardar(Articulos servicio)
    {
        if (!await Existe(servicio.ServicioId))
            return await Insertar(servicio);
        else
            return await Modificar(servicio);
    }

    public async Task<bool> Eliminar(int id)
    {
        var EliminarServicio = await _contexto.Servicio
            .Where(s => s.ServicioId == id)
            .ExecuteDeleteAsync();
        return EliminarServicio > 0;
    }

    public async Task<Articulos?> Buscar(int id)
    {
        return await _contexto.Servicio
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ServicioId == id);
    }

    public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
    {
        return await _contexto.Servicio
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExiteDescripcion(string descripcion, int? servicioId = null)
    {
        if (servicioId.HasValue)
        {
            return await _contexto.Servicio.AnyAsync(s => s.Descripcion == descripcion && s.ServicioId != servicioId);
        }
        else
        {
            return await _contexto.Servicio.AnyAsync(s => s.Descripcion == descripcion);
        }
    }

}
