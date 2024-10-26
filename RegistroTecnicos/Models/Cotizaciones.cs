using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Cotizaciones
{
    [Key]

    [Required(ErrorMessage = "El Campo Trabajo es obligatorio")]
    public int CotizacionId { get; set; }

    [Required(ErrorMessage = "El Campo Cliente es obligatorio")]

    public Clientes? clientes { get; set; }
    [ForeignKey("clientes")]
    public int ClientesId { get; set; }

    [Required(ErrorMessage = "El Campo  Descripcion es obligatorio.")]
    public string? Observacion { get; set; }

    [Required(ErrorMessage = "El Campo Monto es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El Monto debe ser un valor positivo.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "El Campo Fecha es obligatorio.")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    public ICollection<CotizacionesDetalle> CotizacionesDetalle { get; set; }

}
