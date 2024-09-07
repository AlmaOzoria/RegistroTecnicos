using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace RegistroTecnicos.Services;

public class TiposTecnicosServices
{
    private readonly Contexto _contexto;

    public TiposTecnicosServices(Contexto contexto)
    {

       _contexto = contexto; 

    }

    public async Task <bool> Insertar(TiposTecnicos tiposTecnicos)
    {
        _contexto.TiposTecnicos.Add(tiposTecnicos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task <bool> Existe(int tipoTecnicoId)
    {
        return await _contexto.TiposTecnicos.AnyAsync(t => t.TipoTecnicoId == tipoTecnicoId);

    }

    public async Task <bool> Modificar(TiposTecnicos tiposTecnicos)
    {
        _contexto.Update(tiposTecnicos);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task <bool> Guardar(TiposTecnicos tiposTecnicos)
    {
        if (await ExisteDescripcion(tiposTecnicos.Descripcion, tiposTecnicos.TipoTecnicoId))
        { 
            return false;
        }
        if (!await Existe(tiposTecnicos.TipoTecnicoId))
            return await Insertar(tiposTecnicos);
        else
        {
            return await Modificar(tiposTecnicos);
        }
    }

    public async Task <bool> Eliminar(int id)
    {
        var tecnico = await _contexto.TiposTecnicos
            .Where(t => t.TipoTecnicoId == id)
            .ExecuteDeleteAsync();
        return tecnico > 0;
       
    }

    public List<TiposTecnicos> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return _contexto.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }

    public async Task<TiposTecnicos?> Buscar(int id)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoTecnicoId == id);
    }

    public async Task<bool> ExisteDescripcion(string descripcion, int? idTipotecnico = null)
    {
        if (idTipotecnico.HasValue)
        {
            return await _contexto.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion && t.TipoTecnicoId != idTipotecnico);
        }
        else
        {
            return await _contexto.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion);
        }
    }


}
