using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ClientesServices(IDbContextFactory<Contexto> DbFactory)
{


    public async Task<bool> Insertar(Clientes clientes)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Clientes.Add(clientes);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int clientesId)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Clientes.AnyAsync(t => t.ClientesId== clientesId);

    }

    public async Task<bool> Modificar(Clientes clientes)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        _contexto.Update(clientes);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Clientes clientes)
    {
        if (await ExiteNombres(clientes.Nombres, clientes.ClientesId))
        {
            return false;
        }
        if (!await Existe(clientes.ClientesId))
            return await Insertar(clientes);
        else
        {
            return await Modificar(clientes);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        var clientes = await _contexto.Clientes
            .Where(t => t.ClientesId == id)
            .ExecuteDeleteAsync();
        return clientes > 0;

    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }


    public async Task<Clientes?> Buscar(int id)
    {
       
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        return await _contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.ClientesId == id);
    }

    public async Task<bool> ExiteNombres(string nombre, int? clienteId = null)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        if (clienteId.HasValue)
        {
            return await _contexto.Clientes.AnyAsync(t => t.Nombres == nombre && t.ClientesId != clienteId);
        }
        else
        {
            return await _contexto.Clientes.AnyAsync(t => t.Nombres == nombre);
        }
    }
}
