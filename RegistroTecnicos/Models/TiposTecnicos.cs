using System.ComponentModel.DataAnnotations;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor o igual  que 1 ")]
    public int TipoTecnicoId { get; set; }

    [Required(ErrorMessage = " La descripcion es obligatoria.")]
    public string Descripcion { get; set; } = string.Empty;
    public ICollection<Tecnicos> Tecnicos { get; set; } = new List<Tecnicos>();
    
    public Tecnicos? Tecnico { get; set; }
}
