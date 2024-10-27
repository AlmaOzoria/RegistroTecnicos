using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Services;

public class CotizacionServices(IDbContextFactory<Contexto> DbFactory)
{


    public async Task<bool> Insertar(Cotizaciones cotizaciones)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();
        try
        {
            _contexto.Cotizaciones.Add(cotizaciones);
            return await _contexto.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
           
            Console.WriteLine($"Error al insertar: {ex.InnerException?.Message}");
            return false;
        }
    }


    public async Task<bool> Existe(int cotizacionId)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            return await _contexto.Cotizaciones.AnyAsync(c => c.CotizacionId == cotizacionId);

        }

        public async Task<bool> Modificar(Cotizaciones cotizaciones)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            _contexto.Update(cotizaciones);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Cotizaciones cotizaciones)
        {
            if (cotizaciones.CotizacionId == 0) 
            {
                return await Insertar(cotizaciones);
            }
            else
            {
                return await Modificar(cotizaciones);
            }
        }


    public async Task<bool> Eliminar(int id)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            var cotizaciones = await _contexto.Cotizaciones
                .Where(c => c.CotizacionId == id)
                .ExecuteDeleteAsync();
            return cotizaciones > 0;

        }

        public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
        {
            using var _contexto = await DbFactory.CreateDbContextAsync();

            return await _contexto.Cotizaciones
                .AsNoTracking()
                .Include(c => c.clientes)
                .Include(t => t.CotizacionesDetalle)
                .Where(criterio)
                .ToListAsync();
        }


         public async Task<Cotizaciones?> Buscar(int id)
        {
                 await using var _contexto = await DbFactory.CreateDbContextAsync();

                 return await _contexto.Cotizaciones
                .AsNoTracking()
                .Include(c => c.clientes)
                .Include(c => c.CotizacionesDetalle)
                .ThenInclude(td => td.Articulos)
                .FirstOrDefaultAsync(c => c.CotizacionId == id);
        }

        public async Task<List<CotizacionesDetalle>> ObtenerDetallesCotizacionId(int cotizacionId)
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            var detalles = await _contexto.CotizacionesDetalle
                .Where(c => c.cotizaciones.CotizacionId == cotizacionId)
                .ToListAsync();

            return detalles;
        }

        public async Task<List<Clientes>> ObtenerClientes()
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            return await _contexto.Clientes.ToListAsync();
        }

        public async Task<List<Cotizaciones>> ObtenerCotizaciones()
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            return await _contexto.Cotizaciones.ToListAsync();
        }

        public async Task<List<CotizacionesDetalle>> ObtenerDetalle()
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            return await _contexto.CotizacionesDetalle.ToListAsync();

        }
        public async Task<List<Articulos>> ObtenerArticulos()
        {
            await using var _contexto = await DbFactory.CreateDbContextAsync();
            return await _contexto.Articulos.ToListAsync();
        }


}
