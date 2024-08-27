using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }
    [Required(ErrorMessage ="El Campo Nombre es obligatorio")]
    public string? Nombre { get; set; }
    public double SueldoHora { get; set; }
}

