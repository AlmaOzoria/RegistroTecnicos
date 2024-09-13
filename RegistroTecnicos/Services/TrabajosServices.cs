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
        if (await ExiteNombres(trabajos.Nombres, trabajos.TrabajosId))
        {
            return false;
        }
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
            .Where(criterio)
            .ToList();
    }

    public async Task<Trabajos?> Buscar(int id)
    {
        return await _contexto.Trabajos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TrabajosId == id);
    }

    public async Task<bool> ExiteNombres(string nombre, int? trabajosId = null)
    {
        if (trabajosId.HasValue)
        {
            return await _contexto.Trabajos.AnyAsync(t => t.Nombres == nombre && t.TrabajosId != trabajosId);
        }
        else
        {
            return await _contexto.Trabajos.AnyAsync(t => t.Nombres == nombre);
        }
    }
}
