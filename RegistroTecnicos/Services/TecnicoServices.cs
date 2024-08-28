using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoServices
{
    private readonly Contexto _contexto;
       public TecnicoServices(Contexto contexto)
        {
            _contexto = contexto;
        }

    public async Task <bool> Existe(int tecnicoId)
    {
        return await _contexto.Tecnicos.AnyAsync (t => t.TecnicoId == tecnicoId );
    }

    private async Task <bool> Insertar(Tecnicos tecnicos)
    {
        _contexto.Tecnicos.Add(tecnicos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task <bool> Modificar (Tecnicos tecnicos) {
        _contexto.Tecnicos.Update(tecnicos);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(tecnicos).State = EntityState.Detached;
        return modificado;
    }

    public async Task <bool> Guardar(Tecnicos tecnicos)
    {
        if (!await Existe(tecnicos.TecnicoId))
            return await Insertar(tecnicos);
        else
            return await Modificar(tecnicos);
    }

    public async Task <bool> Eliminar (int id)
    {
        var EliminarTecnicos = await _contexto.Tecnicos
            .Where(a => a.TecnicoId == id)
            .ExecuteDeleteAsync();
        return EliminarTecnicos >0 ;
    }

    public async Task <Tecnicos?> Buscar(int id)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == id);
    }

    public async Task <List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return await _contexto.Tecnicos          
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExiteNombre(string nombre, int? tecnicoId = null)
    {
        if (tecnicoId.HasValue)
        {
            return await _contexto.Tecnicos.AnyAsync(t => t.Nombre == nombre && t.TecnicoId != tecnicoId);
        }
        else
        {
            return await _contexto.Tecnicos.AnyAsync(t => t.Nombre == nombre);
        }
    }
        
}
