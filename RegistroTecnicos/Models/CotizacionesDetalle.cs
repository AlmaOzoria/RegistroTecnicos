using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class CotizacionesDetalle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DetalleId { get; set; }

    [Required(ErrorMessage = "El Campo Contizaciones es obligatorio")]

    public Cotizaciones? cotizaciones { get; set; }
    [ForeignKey("cotizaciones")]
    public int CotizacionId { get; set; }


    [Required(ErrorMessage = "El Campo Cantidad es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El Cantidad debe ser un valor positivo.")]
    public decimal Cantidad { get; set; }
    
    [Required(ErrorMessage = "El Campo Precio es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El Precio debe ser un valor positivo.")]
    public decimal? Precio { get; set; }

    public Articulos? Articulos { get; set; }
    [ForeignKey("articulos")]
    public int ArticuloId { get; set; }
}
