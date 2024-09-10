using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]

    [Required(ErrorMessage = "El Campo Cliente es obligatorio")]
    public int ClientesId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El Campo Nombre es obligatorio")]
    public string? Nombres { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "El campo WhatsApp es obligatorio.")]
    public string? WhatsApp { get; set; }
    
}
