using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ClientesServices
{
    private readonly Contexto _contexto;

    public ClientesServices(Contexto contexto)
    {

        _contexto = contexto;

    }

    public async Task<bool> Insertar(Clientes clientes)
    {
        _contexto.Clientes.Add(clientes);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int clientesId)
    {
        return await _contexto.Clientes.AnyAsync(t => t.ClientesId== clientesId);

    }

    public async Task<bool> Modificar(Clientes clientes)
    {
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
        var clientes = await _contexto.Clientes
            .Where(t => t.ClientesId == id)
            .ExecuteDeleteAsync();
        return clientes > 0;

    }

    public List<Clientes> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return _contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }

    public async Task<Clientes?> Buscar(int id)
    {
        return await _contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.ClientesId == id);
    }

    public async Task<bool> ExiteNombres(string nombre, int? clienteId = null)
    {
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
