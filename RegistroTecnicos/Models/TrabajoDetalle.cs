using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class TrabajoDetalle
{
    [Key]


    public int? DetalleId { get; set; }

    [ForeignKey("TrabajoId")]
    public Trabajos? TrabajoId { get; set; }


    [ForeignKey("articuloId")]
    public Articulos? ArticuloId { get; set; }


    [Required(ErrorMessage = "Este Campo debe de ser obligatorio")]
    public decimal? Cantidad { get; set; } 
    
    [Required(ErrorMessage = "Este Campo debe de ser obligatorio")]
    public decimal? Precio { get; set; }

    [Required(ErrorMessage = "Este Campo debe de ser obligatorio")]
    public decimal? Costo { get; set; }

   
}


