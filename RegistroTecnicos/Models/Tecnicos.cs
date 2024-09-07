using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    
    [Required(ErrorMessage ="El Campo Nombre es obligatorio")]
    public string? Nombre { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor o igual que 1.")]
    public int TecnicoId { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "El campo sueldo por hora debe ser mayor que cero.")]
    public double SueldoHora { get; set; }
    public TiposTecnicos? TiposTecnicos { get; set; }


}

