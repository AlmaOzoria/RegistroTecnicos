using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Services;

public class CotizacionServices
{
     private readonly Contexto _contexto;

        public CotizacionServices(Contexto contexto)
        {

            _contexto = contexto;

        }

        public async Task<bool> Insertar(Cotizaciones cotizaciones)
        {
            _contexto.Cotizaciones.Add(cotizaciones);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Existe(int cotizacionId)
        {
            return await _contexto.Cotizaciones.AnyAsync(c => c.CotizacionId == cotizacionId);

        }

        public async Task<bool> Modificar(Cotizaciones cotizaciones)
        {
            _contexto.Update(cotizaciones);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Cotizaciones cotizaciones)
        {

            if (!await Existe(cotizaciones.CotizacionId))
                return await Insertar(cotizaciones);
            else
            {
                return await Modificar(cotizaciones);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var cotizaciones = await _contexto.Cotizaciones
                .Where(c => c.CotizacionId == id)
                .ExecuteDeleteAsync();
            return cotizaciones > 0;

        }

        public List<Cotizaciones> Listar(Expression<Func<Cotizaciones, bool>> criterio)
        {
            return _contexto.Cotizaciones
                .AsNoTracking()
                .Include(c => c.clientes)
                .Include(t => t.CotizacionesDetalle)
                .Where(criterio)
                .ToList();
        }

        public async Task<Cotizaciones?> Buscar(int id)
        {
            return await _contexto.Cotizaciones
                .AsNoTracking()
                .Include(c => c.clientes)
                .Include(c => c.CotizacionesDetalle)
                .ThenInclude(td => td.Articulos)
                .FirstOrDefaultAsync(c => c.CotizacionId == id);
        }

        public async Task<List<CotizacionesDetalle>> ObtenerDetallesCotizacionId(int cotizacionId)
        {
            var detalles = await _contexto.CotizacionesDetalle
                .Where(c => c.cotizaciones.CotizacionId == cotizacionId)
                .ToListAsync();

            return detalles;
        }

        public async Task<List<Clientes>> ObtenerClientes()
        {
            return await _contexto.Clientes.ToListAsync();
        }

        public async Task<List<Cotizaciones>> ObtenerCotizaciones()
        {
            return await _contexto.Cotizaciones.ToListAsync();
        }

        public async Task<List<CotizacionesDetalle>> ObtenerDetalle()
        {
            return await _contexto.CotizacionesDetalle.ToListAsync();

        }
        public async Task<List<Articulos>> ObtenerArticulos()
        {
            return await _contexto.Articulos.ToListAsync();
        }


}
