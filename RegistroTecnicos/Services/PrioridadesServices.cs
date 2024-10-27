using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class PrioridadesServices(IDbContextFactory<Contexto> DbFactory)
{
 
        public async Task<bool> Existe(int prioridadesId)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            return await _contexto.Prioridades.AnyAsync(t => t.PrioridadesId == prioridadesId);
        }

        private async Task<bool> Insertar(Prioridades prioridades)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            _contexto.Prioridades.Add(prioridades);
            return await _contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Prioridades prioridades)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            _contexto.Prioridades.Update(prioridades);
            var modificado = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(prioridades).State = EntityState.Detached;
            return modificado;
        }

        public async Task<bool> Guardar(Prioridades prioridades)
        {
            if (!await Existe(prioridades.PrioridadesId))
                return await Insertar(prioridades);
            else
                return await Modificar(prioridades);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            var EliminarPrioridades = await _contexto.Prioridades
                .Where(a => a.PrioridadesId == id)
                .ExecuteDeleteAsync();
            return EliminarPrioridades > 0;
        }

        public async Task<Prioridades?> Buscar(int id)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            return await _contexto.Prioridades
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.PrioridadesId == id);
        }

        public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            return await _contexto.Prioridades
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

    public async Task<bool> ExisteDescripcion(string descripcion, int? prioridadesId = null)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        if (prioridadesId.HasValue)
        {
            return await _contexto.Prioridades.AnyAsync(t => t.Descripcion == descripcion && t.PrioridadesId != prioridadesId);
        }
        else
        {
            return await _contexto.Prioridades.AnyAsync(t => t.Descripcion == descripcion);
        }
    }


}
